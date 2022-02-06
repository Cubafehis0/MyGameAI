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
            Debug.LogWarning("状态机初始状态不能为空");
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
            Debug.LogError("不能添加空的状态");
            return;
        }
        if (stateDic.ContainsKey(state.type))
        {
            Debug.LogWarning("该状态已经包含有该状态");
            return;
        }
        stateDic.Add(state.type,state);
    }
    public void DeleteState(State state)
    {
        if (!stateDic.ContainsKey(state.type))
        {
            Debug.LogWarning("该状态不包含要移除的状态");
            return;
        }
        stateDic.Remove(state.type);
    }
    public State GetStateByType(StateType type)
    {
        State res = null;
        if(!stateDic.TryGetValue(type,out res))
            Debug.LogWarning(string.Format("该状态机中没有包含{0}状态", type.ToString()));
        return res;
    }

    public void Transition(StateType type)
    {
        State nextState = GetStateByType(type);
        if (nextState == null)
        {
            Debug.LogError("目标状态不存在，状态转移失败");
            return;
        }
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }
}
