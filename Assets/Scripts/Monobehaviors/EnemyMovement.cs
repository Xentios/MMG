using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public TerrainFeatures.TerrainType terrainType;

    [SerializeField]
    LayerMask groundLayerMask;

    Rigidbody2D rb2D;

    private float pushResistTimerSet = 0.4f;
    private float pushResistTimer;

    private float speedModifier = 1f;

    private  float stunTimerSet = 1f;
    private float stunTimer;
    private bool isStunned;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pushResistTimer = pushResistTimerSet;
        stunTimer = stunTimerSet;
        CheckNewTerrain();        
    }

    public void Stun()
    {
        isStunned = true;
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

        switch (terrainType)
        {
            case TerrainFeatures.TerrainType.Default:
            speedModifier = 1f;
            break;
            case TerrainFeatures.TerrainType.Booster:
            speedModifier = 2f;
            break;
            case TerrainFeatures.TerrainType.Ice:
            break;
            case TerrainFeatures.TerrainType.Sand:
            break;
            default:
            break;
        }


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
            if (terrainType != TerrainFeatures.TerrainType.Ice) rb2D.velocity = Vector2.zero;//TODO WARNING this stops other effects.
        }
        else
        {
            rb2D.AddForce(Vector2.left * speedModifier, ForceMode2D.Impulse);
        }
        
    }

  
    public void Push( Vector2 pushForce)
    {
        rb2D.AddForce(pushForce * 10000, ForceMode2D.Force);        
    }

    public void CheckNewTerrain()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,1, groundLayerMask);
        if(hit.collider!=null)
            terrainType = hit.collider.GetComponent<LaneCollider>().terrainType;
    }
 

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 130), "Push Up"))
            Push(Vector2.up);

        if (GUI.Button(new Rect(200, 70, 150, 130), "Push Down"))
            Push(Vector2.down);
    }
}
