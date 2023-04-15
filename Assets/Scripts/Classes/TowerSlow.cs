using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlow : Tower
{
    //public TowerSlow(float timeLimit, bool isTop, float offTimeLimit, float recTime) : base(timeLimit, isTop, offTimeLimit, recTime)
    //{
    //}
    public override bool HandleZomWick(EnemyMovement zomWick)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateLogic(float deltaTime, TowerScript owner)
    {
        base.UpdateLogic(deltaTime,owner);
    }

}