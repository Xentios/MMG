using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStun : Tower
{
    private int ammoCount=2;    
    public TowerStun(float timeLimit, bool isTop) : base(timeLimit, isTop)
    {
    }

    public override bool HandleZomWick(Collider2D collision)
    {
        if (ammoCount <= 0) return false;
        if (effectTimer > 0) return false;

        ammoCount--;
        effectTimer = effectTimerLimit;       
        return true;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (ammoCount > 0) return;

    }

}
