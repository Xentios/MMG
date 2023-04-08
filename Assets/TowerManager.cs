using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    [SerializeField]
    GameObject[] towerPrefabs;

    [SerializeField]
    GameObject[] UITowers;
    

   
    public static void CreateTower(GameObject towerSlot,GameObject towerPrefab)
    {
        
        var towerSlotInfo = towerSlot.GetComponent<TowerSlot>();
        towerSlotInfo.isOccupied = true;        
        var towerInfo = towerPrefab.GetComponent<TowerScript>();
        var offset_y=0f;
        var rotation=Quaternion.identity;
        switch (towerInfo.selectedTowerTypes)
        {
            case TowerScript.TowerTypes.Wind:
            offset_y = 0.7f;
            break;
            case TowerScript.TowerTypes.Stun:
            rotation = towerSlotInfo.isTop == false ? Quaternion.identity : Quaternion.Euler(0, 0, 180);
            break;
            default:
            break;
        }
        //var offset_y = towerInfo.selectedTowerTypes == TowerScript.TowerTypes.Wind ? 0.7f : 0f;
        //var rotation= towerSlotInfo.isTop==false?Quaternion.identity:Quaternion.Euler(0, 0, 180);
        var pos = new Vector3(towerSlot.transform.position.x, towerSlot.transform.position.y+ offset_y, 0);
        Instantiate(towerPrefab, pos, rotation);       
    }

    private void Update()
    {
        
    }
}
