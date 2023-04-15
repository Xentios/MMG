using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerStun")]

public class TowerStun : Tower
{
    public int AmmoCountLimit;
    private int ammoCount;

    //public TowerStun(float timeLimit, bool isTop, float offTimeLimit,float recTime ) : base(timeLimit, isTop, offTimeLimit,recTime)
    //{
    //}


    public override void UpdateLogic2(bool isTop)
    {
        base.UpdateLogic2(isTop);
        ammoCount = AmmoCountLimit;
    }


    public override bool HandleZomWick(EnemyMovement zomWick)
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

    public override void UpdateLogic(float deltaTime,TowerScript owner)
    {
        base.UpdateLogic(deltaTime,owner);
        if (isDisabled==false) return;
        if (isReadyToRecyle == true) return;
        offlineTowerTimer -= deltaTime;
        if (offlineTowerTimer < 0)
        {
            isReadyToRecyle = true;
            //owner.recyleEvent.Invoke(owner);
        }

    }

}


