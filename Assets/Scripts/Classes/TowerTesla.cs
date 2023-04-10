
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTesla : Tower
{
    public TowerTesla(float timeLimit, bool isTop, float offTimeLimit, float recTime) : base(timeLimit, isTop, offTimeLimit, recTime)
    {
    }
    public override bool HandleZomWick(EnemyMovement zomWick)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
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
