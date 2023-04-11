using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public enum Lane
    {
        Bottom,
        NearBottom,       
        Middle,        
        NearTop,
        Top,
    }
    [SerializeField]
    public Lane myLane;

    public TerrainFeatures.TerrainType terrainType;

 

    [SerializeField]
    LayerMask groundLayerMask;
    [SerializeField]
    GameObject WarningSign;
    [SerializeField]
    Animator animator;

    [SerializeField]
    AudioClip[] hurtClips;
    [SerializeField]
    AudioSource hurtAudioSource;
    [SerializeField]
    AudioClip[] walkClips;
    [SerializeField]
    AudioSource walkAudioSource;



    Rigidbody2D rb2D;
 

    private float pushResistTimerSet = 0.4f;
    private float pushResistTimer;

    private float speedModifier = 0.01f;

    private  float stunTimerSet = 1f;
    public float stunTimer;
    public bool isStunned;

    private float actionTimerReset = 5f;
    private float actionTimer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();       
    }
   
    void Start()
    {
        pushResistTimer = pushResistTimerSet;
        stunTimer = stunTimerSet;
        actionTimer = actionTimerReset;        
        StartCoroutine(CheckNewTerrainEvery1Second());
    }

    public void Stun()
    {
        isStunned = true;
        var clip=hurtClips[Random.Range(0, hurtClips.Length)];
        if(clip!=null) hurtAudioSource.PlayOneShot(clip);
    }
    public void Slide()
    {
        animator.SetTrigger("FakeStunned");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rb2D.velocity.x > 0)
        {
            pushResistTimer -= Time.deltaTime; 
        }

        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
        }

        if (stunTimer < 0)
        {
            isStunned = false;
            stunTimer = stunTimerSet;
        }

        if (rb2D.velocity.y <0.2f)
        {
            actionTimer -= Time.deltaTime;
            if (actionTimer < 0)
            {
                actionTimer = actionTimerReset;
                ChooseAction();
            }
        }


        switch (terrainType)
        {
            case TerrainFeatures.TerrainType.Default:
            speedModifier = 0.01f;
            break;
            case TerrainFeatures.TerrainType.Booster:
            speedModifier = 0.01f*6f;
            break;
            case TerrainFeatures.TerrainType.Ice:
            break;
            case TerrainFeatures.TerrainType.Sand:
            break;
            default:
            break;
        }
       
        

    }

    private void ChooseAction()
    {        
        StartCoroutine(FlashWarning());
    }

    IEnumerator FlashWarning()
    {
        for (int i = 0; i < 6; i++)
        {
            WarningSign.SetActive(!WarningSign.activeSelf);
            yield return new WaitForSeconds(0.3f);
        }

        var force = Random.Range(0, 1f) > 0.5f ? Vector2.up : Vector2.down; ;
        switch (myLane)
        {            
            case Lane.Bottom:   force = Vector2.up;
            break;            
            case Lane.Top:      force = Vector2.down;
            break;            
        }
        PushVertical(force);
    }

    private void FixedUpdate()
    {
        if (pushResistTimer < 0f)
        {
            rb2D.velocity = new Vector2(0,rb2D.velocity.y);
            pushResistTimer = pushResistTimerSet;
        }

        if (isStunned)
        {
            if (terrainType != TerrainFeatures.TerrainType.Ice) rb2D.velocity = new Vector2(0,rb2D.velocity.y);
            animator.SetFloat("VelocityX", 0);
        }
        else
        {
            rb2D.AddForce(Vector2.left * speedModifier, ForceMode2D.Impulse);
            animator.SetFloat("VelocityX", rb2D.velocity.x * -1);
        }
    }

    public void PushVertical(Vector2 pushForce)
    {
        pushForce.x = 0;
        Push(pushForce);
    }

    public void PushHorizontal(Vector2 pushForce)
    {
        pushForce.y = 0;
        if (terrainType == TerrainFeatures.TerrainType.Ice) pushForce *= 1.2f;
        Push(pushForce);
    }

    public void Push( Vector2 pushForce)
    {
        switch (pushForce.y)
        {
            case < 0:
            if (myLane == Lane.Bottom) return;
            myLane--;
            break;
            case > 0:
            if (myLane == Lane.Top) return;
            myLane++;
            break;
            default:
            break;
        }
        
        rb2D.AddForce(pushForce * 100, ForceMode2D.Force);        
    }

    public void CheckNewTerrain()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,0.1f, groundLayerMask);
        if (hit.collider != null)
        {   
            terrainType = hit.collider.GetComponent<GroundScript>().terrainType;           
            walkAudioSource.clip = walkClips[(int) terrainType];
            if(walkAudioSource.isPlaying==false) walkAudioSource.Play();
        }
    }
    IEnumerator CheckNewTerrainEvery1Second()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CheckNewTerrain();
        }
    }


#if UNITY_EDITOR
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 170, 150, 130), "Push Up"))
            Push(Vector2.up);

        if (GUI.Button(new Rect(200, 170, 150, 130), "Push Down"))
            Push(Vector2.down);

        if (GUI.Button(new Rect(800, 170, 150, 130), "Push back"))
        {
            rb2D.velocity = Vector2.zero;
            Push(Vector2.right);
        }
            
    }
#endif
}
