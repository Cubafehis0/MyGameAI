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
            Debug.Log("不能添加空的状态转移");
            return;
        }
        if(transitions.Contains(transition))
        {
            Debug.Log("该状态已经包含有该状态转移");
            return;
        }
        transitions.Add(transition); 
    }
    public void DeleteTransition(FSMTransition transition)
    {
        if (!transitions.Contains(transition))
        {
            Debug.Log("该状态不包含要移除的状态转移");
            return;
        }
        transitions.Remove(transition);
    }
    public abstract void Enter();
    public abstract void Excute();
    public abstract void Exit();
    public abstract void CheckTransition();
}
