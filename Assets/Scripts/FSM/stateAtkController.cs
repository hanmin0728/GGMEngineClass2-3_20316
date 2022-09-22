using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateAtkController : MonoBehaviour
{
    public delegate void OnStartStateAtkController();
    public OnStartStateAtkController stateAtkControllerStartHandler;

    public delegate void OnEndStateAtkController();
    public OnEndStateAtkController stateAtkControllerEndHandler;

    public bool getFlagStateAtkController
    {
        get;
        private set;
    }

    private void Start()
    {
        stateAtkControllerStartHandler = new OnStartStateAtkController(stateAtkControllerEnter);
        stateAtkControllerEndHandler = new OnEndStateAtkController(stateAtkControllerEnd);
    }

    private void stateAtkControllerEnter()
    {

    }

    private void stateAtkControllerEnd()
    {

    }
    public void EventStateAtkStart()
    {
        getFlagStateAtkController = true;
        stateAtkControllerStartHandler();
    }
    public void EventStateAtkEnd()
    {
        getFlagStateAtkController = false;
        stateAtkControllerEndHandler();
    }
    public void OnCheckAttackCollider(int attackIndex)
    {
        //GetComponent<IAtkAble>()?.OnExecuteAttack(attackIndex);
    }
}
