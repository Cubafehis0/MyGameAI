using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StateMachine
{
    Dictionary<StateType, State> stateDic;
    State currentState=null;
    public StateMachine()
    {
        stateDic = new Dictionary<StateType, State>();
    }
    public void SetInitalState(StateType type)
    {
        State state = GetStateByType(type);
        if (state == null)
        {
            Debug.LogWarning("״̬����ʼ״̬����Ϊ��");
            return;
        }
        currentState = state;
    }
    public void Excute()
    {
        currentState.Excute();
        currentState.CheckTransition();
    }
    public void AddState(State state)
    {
        if (state== null)
        {
            Debug.LogError("������ӿյ�״̬");
            return;
        }
        if (stateDic.ContainsKey(state.type))
        {
            Debug.LogWarning("��״̬�Ѿ������и�״̬");
            return;
        }
        stateDic.Add(state.type,state);
    }
    public void DeleteState(State state)
    {
        if (!stateDic.ContainsKey(state.type))
        {
            Debug.LogWarning("��״̬������Ҫ�Ƴ���״̬");
            return;
        }
        stateDic.Remove(state.type);
    }
    public State GetStateByType(StateType type)
    {
        State res = null;
        if(!stateDic.TryGetValue(type,out res))
            Debug.LogWarning(string.Format("��״̬����û�а���{0}״̬", type.ToString()));
        return res;
    }

    public void Transition(StateType type)
    {
        State nextState = GetStateByType(type);
        if (nextState == null)
        {
            Debug.LogError("Ŀ��״̬�����ڣ�״̬ת��ʧ��");
            return;
        }
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }
}
