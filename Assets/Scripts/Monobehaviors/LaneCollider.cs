using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneCollider : MonoBehaviour
{
    [SerializeField]
    EnemyMovement.Lane myLane;

    private void Start()
    {
        myLane = (EnemyMovement.Lane) transform.position.y+3;//TODO remove this later    
    }


    private void OnTriggerExit2D(Collider2D collision)
    {       
        if (collision.CompareTag("ZomWick")) {
            
            var rb2D=collision.gameObject.GetComponent<Rigidbody2D>();
            var zomWick=rb2D.GetComponent<EnemyMovement>();
            int direction = (int) Mathf.Sign(rb2D.velocity.y);
            if(zomWick.myLane==myLane+direction)            
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);           
        }
    }

  
   
}
