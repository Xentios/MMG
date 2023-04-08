using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStun : Tower
{
    private int ammoCount=2;
    public TowerStun(float timeLimit, bool isTop) : base(timeLimit, isTop)
    {
    }

    public override void HandleZomWick(Collider2D collision)
    {
        if (ammoCount <= 0) return;
        if (effectTimer > 0) return;

        ammoCount--;
        var ZomWick = collision.gameObject.GetComponent<EnemyMovement>();
        if (ZomWick.terrainType != TerrainFeatures.TerrainType.Ice)
        {
            ZomWick.Stun();
        }

        throw new System.NotImplementedException();
    }
}
