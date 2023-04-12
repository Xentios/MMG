using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    
    [SerializeField]
    private float speed;
    [SerializeField]
    private float strenght;

    void Update()
    {
        transform.position += Vector3.right * speed  * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZomWick"))
        {
            var ZomWick = collision.GetComponent<EnemyMovement>();
            ZomWick.PushHorizontal(Vector2.right * strenght);
            Destroy(gameObject);
        }
    }
}
