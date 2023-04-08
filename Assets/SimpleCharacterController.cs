using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    private float moveSpeed=1;
    [SerializeField]
    private float shootTimerLimit = 10;
    private float shootTimer;

    void Start()
    {
        //shootTimer = shootTimerLimit;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Go Up");
            var y = transform.position.y + (moveSpeed * 154);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Go Down");
            var y = transform.position.y + (moveSpeed * -154);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shoot");
            Shoot();
        }

    }

    private void Shoot()
    {
        if (shootTimer > 0) return;
        Instantiate(Bullet,transform.position,Quaternion.identity);
        shootTimer = shootTimerLimit;
    }
}
