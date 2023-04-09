using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 


public abstract class Tower
{
    public float effectTimerLimit;
    protected float effectTimer;
    protected bool isTopSide;

    public float offlineTowerTimerLimit;
    protected float offlineTowerTimer;

    public float recyleTimer;

    public bool isDisabled;
    public bool isReadyToRecyle;

    public UnityEvent disabledEvent;
    public UnityEvent<Tower> recyleEvent;


    public Tower(float timeLimit, bool isTop, float offTimeLimit)
    {
        offlineTowerTimerLimit = offTimeLimit;
        effectTimerLimit = timeLimit;
        offlineTowerTimer = offlineTowerTimerLimit;
        isTopSide = isTop;
        disabledEvent = new UnityEvent();
        recyleEvent = new UnityEvent<Tower>();
        recyleTimer = 3f;
    }
      
    public abstract bool HandleZomWick(Collider2D collision);


    public virtual void Update(float deltaTime)
    {
        effectTimer -= deltaTime;       
    }
    
}
