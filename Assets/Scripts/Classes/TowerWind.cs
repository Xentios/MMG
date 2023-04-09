using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerWind : Tower
{
    
    public TowerWind(float timeLimit, bool isTop, float offTimeLimit) : base(timeLimit, isTop, offTimeLimit)
    {
    }

    public override bool HandleZomWick(Collider2D collision)
    {
        if (effectTimer > 0) return false;
        if (isDisabled == true) return false;

        var ZomWick = collision.gameObject.GetComponent<EnemyMovement>();
        if (ZomWick.terrainType == TerrainFeatures.TerrainType.Sand) return false;

        Vector2 force = isTopSide ? Vector2.down : Vector2.up;
        ZomWick.Push(force);
        effectTimer = effectTimerLimit;
        return false;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        offlineTowerTimer -= deltaTime;
       
        if (offlineTowerTimer < 0 && isDisabled==false)
        {
            isDisabled = true;
            disabledEvent.Invoke();
        }
       

    }
}
