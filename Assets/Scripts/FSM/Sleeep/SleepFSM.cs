using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepFSM : MonoBehaviour
{
    protected StateMachine<SleepFSM> sleeepFSM;
    public StateMachine<SleepFSM> SLEEPFSM => sleeepFSM;

    public float pigondo = 0f;
    public float bagopa = 0f;

    private void Start()
    {
        sleeepFSM = new StateMachine<SleepFSM>(this, new stateSleep()); //콜싸인
        sleeepFSM.AddStateList(new stateEat()); //관리자 목록 등혹
        sleeepFSM.AddStateList(new stateGame());
    }



    private void Update()
    {
        sleeepFSM.Update(Time.deltaTime); //시간바꿔서 줘보기  
        Debug.Log(sleeepFSM.getNowState);
    }
}
