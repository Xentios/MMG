using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public Tower towerType;

    [SerializeField]
    public bool isTop;
    [SerializeField]
    public TowerTypes selectedTowerTypes;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private Transform visual;
    [SerializeField]
    private Transform secondaryVisual;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip shotSound;

    private AudioSource audioSource;
    public enum TowerTypes
    {
        Wind,
        Stun
    }

    private void Awake()
    {
        switch (selectedTowerTypes)
        {
            case TowerTypes.Wind:
            towerType = new TowerWind(3f, isTop,5f,3f);
            break;
            case TowerTypes.Stun:
            towerType = new TowerStun(3f, isTop,6f,6f);
            break;
            default:
            break;
        }
        towerType.disabledEvent.AddListener(OnTowerDisable);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shotSound;
    }

    private void OnDestroy()
    {
        towerType?.disabledEvent.RemoveListener(OnTowerDisable);
    }

    private void OnTowerDisable()
    {
        audioSource.Stop();       
        animator.SetBool("Disabled",true);
        if(secondaryVisual!=null) secondaryVisual.gameObject.SetActive(false);
        StartCoroutine(ScaleOverTime());
    }

    
    
    private IEnumerator ScaleOverTime()
    {
        float duration= towerType.recyleTimer/2;
        float elapsedTime = 0;
        float startScale = 1f;
        float endScale = 0.5f;

        yield return new WaitForSeconds(towerType.recyleTimer / 2);
        while (elapsedTime < duration)
        {           
            float currentScale = Mathf.Lerp(startScale, endScale, elapsedTime / duration);           
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
                
        transform.localScale = new Vector3(endScale, endScale, endScale);
    }


    void Update()
        {
            towerType.Update(Time.deltaTime);        
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("ZomWick")){
            var zomWick = collision.GetComponent<EnemyMovement>();
                if (towerType.HandleZomWick(zomWick))
                {
                    //How to Instantiate from normal classes with a good design? 
                    Instantiate(rockPrefab, transform.position, Quaternion.identity);
                    animator.SetTrigger("Hit");             
                    audioSource.Play();
                }            
            }
        }
}
