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

        towers = new List<TowerScript>();
    }

    [SerializeField]
    GameObject[] towerPrefabs;

    [SerializeField]
    GameObject[] UITowers;
    
    static List<TowerScript> towers;

    public void OnDestroy()
    {
        foreach (var tower in towers)
        {
           // tower.recyleEvent.RemoveListener(RecyleTower);
        }
    }

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
        var pos = new Vector3(towerSlot.transform.position.x, towerSlot.transform.position.y+ offset_y, 0);
        var newTower=Instantiate(towerPrefab, pos, rotation).GetComponent<TowerScript>();
        //newTower.recyleEvent.AddListener(RecyleTower);
        towers.Add(newTower);


    }

    public static void RecyleTower()
    {

    }

    private void Update()
    {
        
    }
}
