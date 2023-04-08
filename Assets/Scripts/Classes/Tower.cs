using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Tower
{
    public float effectTimerLimit;
    protected float effectTimer;
    protected bool isTopSide;
    
    public Tower(float timeLimit, bool isTop)
    {
        effectTimerLimit = timeLimit;
        isTopSide = isTop;
    }
      
    public abstract bool HandleZomWick(Collider2D collision);


    public void Update(float deltaTime)
    {
        effectTimer -= deltaTime;
    }
    
}
