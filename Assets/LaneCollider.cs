using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision Dedected");
        if (collision.CompareTag("ZomWick")) {
            var rb2D=collision.gameObject.GetComponent<Rigidbody2D>();
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
        }
    }
}
