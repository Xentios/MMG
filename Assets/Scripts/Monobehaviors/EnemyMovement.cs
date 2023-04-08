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


    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pushResistTimer = pushResistTimerSet;
        CheckNewTerrain();        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2D.velocity.x > 0)
        {
            pushResistTimer -= Time.deltaTime;
        }

        switch (terrainType)
        {
            case TerrainFeatures.TerrainType.Default:
            speedModifier = 1f;
            break;
            case TerrainFeatures.TerrainType.Booster:
            speedModifier = 4f;
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
        rb2D.AddForce(Vector2.left*speedModifier,ForceMode2D.Impulse);
    }

    public void PushHorizantal()
    {
       
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
