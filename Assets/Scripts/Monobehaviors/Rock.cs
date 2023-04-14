using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;


    private float way;

    private float randomStopRange;

    private float randomSpeedModifier;

    private bool isDisabled;
    void Start()
    {
        way =Mathf.Sign( -1 - transform.position.y);
        randomStopRange = -1f;
        randomStopRange += Random.Range(-0.5f, 0.5f);       
        randomSpeedModifier = Random.Range(0.9f, 1f); 
    }

    
    void Update()
    {       
        if (isDisabled) return;       
        transform.position =new Vector2(transform.position.x, transform.position.y+ way*randomSpeedModifier *speed* Time.deltaTime);       
       if( ((way==1) &&transform.position.y> randomStopRange ) ||( way==-1 && transform.position.y < randomStopRange))
        {
            StopInAir();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        var ZomWick = collision.gameObject.GetComponent<EnemyMovement>();
        if (ZomWick.terrainType != TerrainFeatures.TerrainType.Ice)
        {
            ZomWick.Stun();
        }
        else
        {
            ZomWick.Slide();
        }
        isDisabled = true;
        
        GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine(StopInTime());   
    }

    private void StopInAir()
    {        
        isDisabled = true;
        GetComponent<CircleCollider2D>().enabled = false;              
        transform.rotation=Quaternion.Euler(new Vector3(0,0, Random.rotation.eulerAngles.y));     
        transform.localScale *= Random.Range(0.5f, 0.9f);
        StopLogic();
    }

    IEnumerator StopInTime()
    {       
        var rb2D = GetComponent<Rigidbody2D>();
        Vector2 randomVector = Random.insideUnitCircle;
        rb2D.AddRelativeForce(randomVector*0.001f);     
        rb2D.drag = 1.2f;
        yield return new WaitForSeconds(0.3f);
        StopLogic();     
     
    }

    private void StopLogic()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        animator.SetTrigger("Hit");
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;    
        Camera.main.DOShakePosition(0.1f,0.1f,4,90,false);
    }
}
