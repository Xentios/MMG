using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private Tower towerType;

    [SerializeField]
    private TowerTypes SelectedTowerTypes;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private Transform visual;
    enum TowerTypes
    {
        Wind,
        Stun
    }

    private void Awake()
    {
        switch (SelectedTowerTypes)
        {
            case TowerTypes.Wind:
            towerType = new TowerWind(3f, true);
            break;
            case TowerTypes.Stun:
            towerType = new TowerStun(3f, true);
            break;
            default:
            break;
        }
        
    }

    void Start()
    {

       // visual = GetComponentInChildren<Transform>();
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
            }            
        }
    }
}
