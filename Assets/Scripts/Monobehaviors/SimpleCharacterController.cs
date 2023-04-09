using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    
    [SerializeField]
    private EnemyMovement.Lane myLane;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    private float moveSpeed=1;

    [SerializeField]
    private float shootTimerLimit = 10;
    public float shootTimer;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] shootingSounds;
    void Start()
    {
       // shootTimer = shootTimerLimit;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // Debug.Log("Shoot");
            Shoot();
        }

    }

    private void Shoot()
    {
        if (shootTimer > 0) return;
        Instantiate(Bullet,transform.position,Quaternion.identity);
        shootTimer = shootTimerLimit;
        animator.SetTrigger("Shoot");
        var soundClip = shootingSounds[Random.Range(0, shootingSounds.Length)];
        audioSource.clip = soundClip;
        audioSource.Play();
    }

    private void MoveUp()
    {
        if (myLane == EnemyMovement.Lane.Top) return;
        myLane++;
        //Debug.Log("Go Up");
        var y = transform.position.y + (moveSpeed * 1);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void MoveDown()
    {
        if (myLane == EnemyMovement.Lane.Bottom) return;
        myLane--;
       // Debug.Log("Go Down");
        var y = transform.position.y + (moveSpeed * -1);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
