using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float speed;
    [SerializeField]
    private float strenght;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed *100 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZomWick"))
        {
            var ZomWick = collision.GetComponent<EnemyMovement>();
            ZomWick.Push(Vector2.right * strenght);
            Destroy(gameObject);
        }
    }
}
