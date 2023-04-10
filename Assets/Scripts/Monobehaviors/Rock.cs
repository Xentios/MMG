using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        randomSpeedModifier = Random.Range(1, 1f);
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
        var rb2D = GetComponent<Rigidbody2D>();
        isDisabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
        animator.SetTrigger("Hit");
        rb2D.velocity = Vector2.zero;
    }

    IEnumerator StopInTime()
    {
        //TODO  make more juicy.
        var rb2D = GetComponent<Rigidbody2D>();
        Vector2 randomVector = Random.insideUnitCircle;
        rb2D.AddRelativeForce(randomVector*0.001f);
        rb2D.drag = 1.2f;
        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("Hit");
        //TODO Super Hero Landing Sound
        rb2D.velocity = Vector2.zero;
    }
}
