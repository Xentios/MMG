
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTesla : Tower
{
    //public TowerTesla(float timeLimit, bool isTop, float offTimeLimit, float recTime) : base(timeLimit, isTop, offTimeLimit, recTime)
    //{
    //}
    public override bool HandleZomWick(EnemyMovement zomWick)
    {

        if (effectTimer > 0) return false;
        disabledEvent.Invoke();
        return false;
    }

    public override void UpdateLogic(float deltaTime, TowerScript owner)
    {
        base.UpdateLogic(deltaTime, owner);
        //offlineTowerTimer -= deltaTime;

        //if (offlineTowerTimer < 0 && isDisabled == false)
        //{
        //    isDisabled = true;
        //    disabledEvent.Invoke();
        //}
        //if (recyleTimer > 0 && isDisabled && isReadyToRecyle == false)
        //{
        //    recyleTimer -= deltaTime;
        //    if (recyleTimer <= 0)
        //    {
        //        isReadyToRecyle = true;
        //        recyleEvent.Invoke(this);
        //    }

        //}

    }
}
