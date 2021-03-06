using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    [System.Serializable]
    public abstract class Node
    {
        protected NodeState nodeState;
        public NodeState NodeState { get => nodeState; }
        public abstract NodeState Evaluate();
    }
    public enum NodeState
    {
        RUNNING,
        FAILURE,
        SUCCESS
    }
}