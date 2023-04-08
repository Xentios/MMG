using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private Tower towerType;

    [SerializeField]
    public bool isTop;
    [SerializeField]
    public TowerTypes selectedTowerTypes;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private Transform visual;

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
            towerType = new TowerWind(3f, isTop);
            break;
            case TowerTypes.Stun:
            towerType = new TowerStun(3f, isTop);
            break;
            default:
            break;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shotSound;
    }


    
    void Update()
    {
        towerType.Update(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ZomWick")){
            if (towerType.HandleZomWick(collision))
            {
                Instantiate(rockPrefab, transform.position, Quaternion.identity);
                audioSource.Play();
            }            
        }
    }
}
