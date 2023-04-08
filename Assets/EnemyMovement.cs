using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb2D.AddForce(Vector2.left*1,ForceMode2D.Impulse);
    }

    public void Push( Vector2 pushForce)
    {
        rb2D.AddForce(pushForce * 10000, ForceMode2D.Force);
    }

 

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 130), "Push Up"))
            Push(Vector2.up);

        if (GUI.Button(new Rect(200, 70, 150, 130), "Push Down"))
            Push(Vector2.down);
    }
}
