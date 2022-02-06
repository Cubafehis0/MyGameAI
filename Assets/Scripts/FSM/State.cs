using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StateType
{
    Chase,
    Patrol,
}
public abstract class State
{
    public StateType type;

    protected List<FSMTransition> transitions;
    protected StateMachine stateMachine;
    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void AddTransition(FSMTransition transition)
    {
        if(transition==null)
        {
            Debug.Log("������ӿյ�״̬ת��");
            return;
        }
        if(transitions.Contains(transition))
        {
            Debug.Log("��״̬�Ѿ������и�״̬ת��");
            return;
        }
        transitions.Add(transition); 
    }
    public void DeleteTransition(FSMTransition transition)
    {
        if (!transitions.Contains(transition))
        {
            Debug.Log("��״̬������Ҫ�Ƴ���״̬ת��");
            return;
        }
        transitions.Remove(transition);
    }
    public abstract void Enter();
    public abstract void Excute();
    public abstract void Exit();
    public abstract void CheckTransition();
}
