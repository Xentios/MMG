using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private float speed;


    private float way;

    private float randomStopRange;

    private float randomSpeedModifier;

    private bool isDisabled;
    void Start()
    {
        way =Mathf.Sign( -1 - transform.position.y);
        randomStopRange = Random.Range(0, -3*way);
        randomSpeedModifier = Random.Range(1, 1f);
    }

    // Update is called once per frame
    void Update()
    {       
        if (isDisabled) return;       
        //transform.position =new Vector2(transform.position.x, transform.position.y+ 0.001f * Time.deltaTime);
        if (transform.position.y < randomStopRange) isDisabled = true;
    }

    private void FixedUpdate()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up/1000f, ForceMode2D.Force);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        var ZomWick = collision.gameObject.GetComponent<EnemyMovement>();
        if (ZomWick.terrainType != TerrainFeatures.TerrainType.Ice)
        {
            ZomWick.Stun();
        }
        isDisabled = true;
        
        GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine(StopInTime());   
    }

    IEnumerator StopInTime()
    {
        //TODO  make more juicy.
        var rb2D = GetComponent<Rigidbody2D>();
        Vector2 randomVector = Random.insideUnitCircle;
        rb2D.AddRelativeForce(randomVector*2f);
        rb2D.drag = 1.2f;
        yield return new WaitForSeconds(0.3f);
        rb2D.velocity = Vector2.zero;
    }
}
