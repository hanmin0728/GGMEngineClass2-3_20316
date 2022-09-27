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
        sleeepFSM = new StateMachine<SleepFSM>(this, new stateSleep()); //�ݽ���
        sleeepFSM.AddStateList(new stateEat()); //������ ��� ��Ȥ
        sleeepFSM.AddStateList(new stateGame());
    }



    private void Update()
    {
        sleeepFSM.Update(Time.deltaTime); //�ð��ٲ㼭 �ຸ��  
        Debug.Log(sleeepFSM.getNowState);
    }
}
