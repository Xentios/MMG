using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    [SerializeField]
    private float touch_offset=0.5f;

    [SerializeField]
    private EnemyMovement.Lane myLane;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    private float moveSpeed=1;

    //[SerializeField]
    //private float shootTimerLimit = 10;
    public FloatReference gunReloadTimer;
    private float shootTimer;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] shootingSounds;

    [SerializeField]
    private AudioSource reloadingSound;

    private void Start()
    {
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;


#if UNITY_ANDROID       
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Began) continue;

                if (touch.position.x < Screen.width / 4)
                {
                    
                        var touchWorldY=Camera.main.ScreenToWorldPoint(touch.position, Camera.main.stereoActiveEye).y;
                       
                        if (touchWorldY < transform.position.y- touch_offset)
                        {
                            MoveDown();
                        }
                        else if(touchWorldY > transform.position.y + touch_offset)
                        {
                            MoveUp();
                        }
                    
                    
                    
                }
                else if (touch.position.x > Screen.width *3/ 4 )
                {
                    Shoot();
                }
            }
        }
#else

        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.RightControl))
        {           
            Shoot();
        }
#endif

            if (shootTimer > 0f && shootTimer < 1f&& reloadingSound.isPlaying==false)
        {
            reloadingSound.Play();
        }


    }

    private void Shoot()
    {
        if (shootTimer > 0) return;
        Instantiate(Bullet,transform.position,Quaternion.identity);
        shootTimer = gunReloadTimer;
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

    public void MoveVisualAway()
    {
        //TODO maybe?
    }

    public void MoveVisualFront()
    {
        //TODO maybe?
    }
}
