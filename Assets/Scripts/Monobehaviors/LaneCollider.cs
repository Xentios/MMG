using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneCollider : MonoBehaviour
{



    private void OnTriggerExit2D(Collider2D collision)
    {       
        if (collision.CompareTag("ZomWick")) {
            var rb2D=collision.gameObject.GetComponent<Rigidbody2D>();
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            //var ZomWick = collision.gameObject.GetComponent<EnemyMovement>();
            //ZomWick.CheckNewTerrain();
        }
    }

  
   
}
