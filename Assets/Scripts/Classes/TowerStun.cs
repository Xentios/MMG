using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStun : Tower
{
    private int ammoCount=2;

    public TowerStun(float timeLimit, bool isTop, float offTimeLimit,float recTime ) : base(timeLimit, isTop, offTimeLimit,recTime)
    {
    }



    public override bool HandleZomWick(Collider2D collision)
    {
        if (ammoCount <= 0) return false;
        if (effectTimer > 0) return false;

        ammoCount--;
        effectTimer = effectTimerLimit;
        if (ammoCount <= 0)
        {
            isDisabled = true;
            disabledEvent.Invoke();
        }       
        return true;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (isDisabled==false) return;
        if (isReadyToRecyle == true) return;
        offlineTowerTimer -= deltaTime;
        if (offlineTowerTimer < 0)
        {
            isReadyToRecyle = true;
            recyleEvent.Invoke(this);
        }

    }

}
