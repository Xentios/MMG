using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;




[Serializable]
public abstract class Tower:ScriptableObject
{
    public GameObject projectilePrefab;

    public float effectTimerLimit;
    [NonSerialized]
    protected float effectTimer;
    [NonSerialized]
    public bool isTopSide;

    public float offlineTowerTimerLimit;
    [NonSerialized]
    protected float offlineTowerTimer;

    public float RecTime;
    [NonSerialized]
    public float recyleTimer;
    [NonSerialized]
    public bool isDisabled;
    [NonSerialized]
    public bool isReadyToRecyle;

    [NonSerialized]
    public UnityEvent disabledEvent;

    public virtual void UpdateLogic2(bool isTop)
    {
        Debug.Log("AWaken");
        
        offlineTowerTimer = offlineTowerTimerLimit;
        effectTimer = effectTimerLimit;
        offlineTowerTimer = offlineTowerTimerLimit;
        isTopSide = isTop;
        disabledEvent = new UnityEvent();
        //recyleEvent = new UnityEvent<Tower>();
        recyleTimer = RecTime;
    }



    //public Tower(float timeLimit, bool isTop, float offTimeLimit,float recTime)
    //{
    //    offlineTowerTimerLimit = offTimeLimit;
    //    effectTimerLimit = timeLimit;
    //    offlineTowerTimer = offlineTowerTimerLimit;
    //    isTopSide = isTop;
    //    disabledEvent = new UnityEvent();
    //    recyleEvent = new UnityEvent<Tower>();
    //    recyleTimer = recTime;
    //}

    //protected Tower(float timeLimit, bool isTop, float offTimeLimit)
    //{
    //    this.timeLimit = timeLimit;
    //    this.isTop = isTop;
    //    this.offTimeLimit = offTimeLimit;
    //}

    public abstract bool HandleZomWick(EnemyMovement zomWick);


    public virtual void UpdateLogic(float deltaTime, TowerScript owner)
    {
        effectTimer -= deltaTime;       
    }

   

}


