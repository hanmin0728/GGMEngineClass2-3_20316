using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> 
{
    protected StateMachine<T> stateMachine; //상태들을 관리하는 애, 타입에 따라 바뀜
    protected T stateMachineClass;

    public State() { } //생성자

    /// <summary>
    /// 해당 상태머신과 상태머신 호출 클래스 설정
    /// </summary>
    internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
    {
        this.stateMachine = stateMachine; //해당 상태머신 설정
        this.stateMachineClass = stateMachineClass; 

        OnAwake(); //각 상태 초기화 진행
    }

    public virtual void OnAwake() { } //각 상태 초기화 진행
    public virtual void OnStart() { } //각 상태에 들어왔을때
    public abstract void OnUpdate(float deltaTime); // 각 상태에서 반드시 실행해야 하는 로직
    public virtual void OnEnd() { } // 각 상태 종료시 끝나고 애니메이션꺼주고 네브메시 정리해주고 

}
public sealed class StateMachine<T> //sealed 내꺼 고치지마 부모꺼를 재정의 하지마 한정자
{
    private T stateMachine; //호출로 사용할 상태 머신 클래스
    //key = generic type(클래스(스크립트) 이름), value = state
    private Dictionary<System.Type, State<T>> stateLists = new Dictionary<Type, State<T>>();

    private State<T> nowState; //현재 상태값
    public State<T> getNowState => nowState;

    private State<T> beforeState; //이전상태값
    public State<T> getBeforeState => beforeState;

    private float stateDurationTime = 0f;
    public float getStateDurationTime => stateDurationTime;

    public StateMachine(T stateMachine, State<T> initState)
    {
        this.stateMachine = stateMachine;

        AddStateList(initState); //상태가 초기화 된 것을 등록한다
        nowState = initState;
        nowState.OnStart(); //현재 상태를 진행한다
    }
    /// <summary>
    /// 상태 관리자에 상태를 등록한다 단순히 목록에 넣어주는애
    /// </summary>
    public void AddStateList(State<T> state)
    {
        state.SetMachineWithClass(this, stateMachine); 
        stateLists[state.GetType()] = state; 
    }

    public void Update(float deltaTime)
    {
        stateDurationTime += deltaTime;
        nowState.OnUpdate(deltaTime);
    }

    public Q ChangeState<Q>() where Q : State<T>    
    {
        var newType = typeof(Q);

        if (nowState.GetType() == newType)
        {
            return nowState as Q; 
        }
        if (nowState != null)
        {
            nowState.OnEnd();
        }
        beforeState = nowState;
        nowState = stateLists[newType];

        nowState.OnStart();
        stateDurationTime = 0f;

        return nowState as Q;
    }
}
