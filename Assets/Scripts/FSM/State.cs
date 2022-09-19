using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> 
{
    protected StateMachine<T> stateMachine; //���µ��� �����ϴ� ��, Ÿ�Կ� ���� �ٲ�
    protected T stateMachineClass;

    public State() { } //������

    /// <summary>
    /// �ش� ���¸ӽŰ� ���¸ӽ� ȣ�� Ŭ���� ����
    /// </summary>
    internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
    {
        this.stateMachine = stateMachine; //�ش� ���¸ӽ� ����
        this.stateMachineClass = stateMachineClass; 

        OnAwake(); //�� ���� �ʱ�ȭ ����
    }

    public virtual void OnAwake() { } //�� ���� �ʱ�ȭ ����
    public virtual void OnStart() { } //�� ���¿� ��������
    public abstract void OnUpdate(float deltaTime); // �� ���¿��� �ݵ�� �����ؾ� �ϴ� ����
    public virtual void OnEnd() { } // �� ���� ����� ������ �ִϸ��̼ǲ��ְ� �׺�޽� �������ְ� 

}
public sealed class StateMachine<T> //sealed ���� ��ġ���� �θ𲨸� ������ ������ ������
{
    private T stateMachine; //ȣ��� ����� ���� �ӽ� Ŭ����
    //key = generic type(Ŭ����(��ũ��Ʈ) �̸�), value = state
    private Dictionary<System.Type, State<T>> stateLists = new Dictionary<Type, State<T>>();

    private State<T> nowState; //���� ���°�
    public State<T> getNowState => nowState;

    private State<T> beforeState; //�������°�
    public State<T> getBeforeState => beforeState;

    private float stateDurationTime = 0f;
    public float getStateDurationTime => stateDurationTime;

    public StateMachine(T stateMachine, State<T> initState)
    {
        this.stateMachine = stateMachine;

        AddStateList(initState); //���°� �ʱ�ȭ �� ���� ����Ѵ�
        nowState = initState;
        nowState.OnStart(); //���� ���¸� �����Ѵ�
    }
    /// <summary>
    /// ���� �����ڿ� ���¸� ����Ѵ� �ܼ��� ��Ͽ� �־��ִ¾�
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
