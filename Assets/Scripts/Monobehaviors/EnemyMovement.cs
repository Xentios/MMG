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
    [SerializeField]
    AudioSource bulletHurtAudioSource;

    [SerializeField]
    Animator visualFX;


    private Rigidbody2D rb2D;
 

    private float pushResistTimerSet = 0.4f;
    private float pushResistTimer;

    //private float speedModifier = 0.01f;

    public FloatReference speedModifier;

    private  float stunTimerSet = 1f;
    public float stunTimer;
    private bool isStunned;
    private bool isStunApplied = true;

    private float actionTimerReset = 5f;
    private float actionTimer;
    private bool readyToChangeLane;

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
        rb2D.velocity = Vector2.left * 2;
    }

    public void Stun()
    {
        isStunned = true;
        isStunApplied = false;
        var clip=hurtClips[Random.Range(0, hurtClips.Length)];
        if(clip!=null) hurtAudioSource.PlayOneShot(clip);
    }

    public void StopZomWick()
    {
        isStunned = true;
        stunTimer = 10f;
        rb2D.velocity = Vector2.zero;
        rb2D.simulated = false;
    }

    public void Slide()
    {
        animator.SetTrigger("FakeStunned");
    }
       
    void Update()
    {
        
        
        if (rb2D.velocity.x > 0f)
        {
            pushResistTimer -= Time.deltaTime;            
        }
       

        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            animator.SetFloat("VelocityX", 0);
            visualFX.gameObject.SetActive(false);
        }
        else
        {
            visualFX.gameObject.SetActive(true);
            animator.SetFloat("VelocityX", rb2D.velocity.x * -1);
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


       
    }

    private void FixedUpdate()
    {

        if (pushResistTimer < 0f)
        {
            rb2D.velocity = new Vector2(-2, rb2D.velocity.y);
            pushResistTimer = pushResistTimerSet;
        }

        if (isStunned)
        {
            if (isStunApplied == false && rb2D.velocity.x < 0f)
            {
                if (terrainType != TerrainFeatures.TerrainType.Ice) rb2D.velocity = new Vector2(0, rb2D.velocity.y);
                if (terrainType == TerrainFeatures.TerrainType.Booster) rb2D.velocity = new Vector2(-2, rb2D.velocity.y);
                isStunApplied = true;
            }
        }
        else
        {
           // rb2D.AddForce(Vector2.left * speedModifier, ForceMode2D.Impulse);
           

            if (readyToChangeLane) ChangeLane();
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

        while (isStunned)
        {
            WarningSign.SetActive(true);
            yield return null;
        }
        WarningSign.SetActive(false);
        readyToChangeLane = true;
       
    }

    private void ChangeLane()
    {
        readyToChangeLane = false;

        var force = Random.Range(0, 1f) > 0.5f ? Vector2.up : Vector2.down; ;

        //Making wick change 2 lanes if random is 2 and made it 1/4 change to happen
        var random=  Random.Range(1, 3);
        random = Random.Range(0, 2) == 0 ? 1 : random; 
        switch (myLane)
        {
            case Lane.Bottom:
            force = Vector2.up* random;
            myLane += random - 1;
            break;
            case Lane.Top:
            force = Vector2.down* random;
            myLane -= random - 1;
            break;
        }
        PushVertical(force);
        
    }

 

    public void PushVertical(Vector2 pushForce)
    {
        pushForce.x = 0;
        Push(pushForce);
    }

    public void PushHorizontal(Vector2 pushForce)
    {
        bulletHurtAudioSource.pitch = Random.Range(0.3f, 0.8f);
        bulletHurtAudioSource.Play();
        pushForce.y = 0;       
        rb2D.velocity = new Vector2(0, rb2D.velocity.y); 
        if (terrainType == TerrainFeatures.TerrainType.Ice) pushForce *= 1.2f;
        if (terrainType == TerrainFeatures.TerrainType.Sand) pushForce *= 0.8f;
        pushResistTimer = pushResistTimerSet;
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
            var ground = hit.collider.GetComponent<GroundScript>();
            terrainType = ground.terrainType;
            speedModifier = ground.terrainSpeed;

            walkAudioSource.clip = walkClips[(int) terrainType];
            if(walkAudioSource.isPlaying==false) walkAudioSource.Play();
            visualFX.SetInteger("TerrainType", (int) terrainType);
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
            Push(Vector2.right*12);
        }
            
    }
#endif
}
