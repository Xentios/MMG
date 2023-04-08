using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWind : Tower
{
    public TowerWind(float timeLimit, bool isTop) : base(timeLimit, isTop)
    {
    }

    public override void HandleZomWick(Collider2D collision)
    {
        if (effectTimer > 0) return;

        var ZomWick = collision.gameObject.GetComponent<EnemyMovement>();
        if (ZomWick.terrainType == TerrainFeatures.TerrainType.Sand) return;

        Vector2 force = isTopSide ? Vector2.down : Vector2.up;
        ZomWick.Push(force);
        effectTimer = effectTimerLimit;
    }
}
