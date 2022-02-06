using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    public class Sequence : Node
    {
        public List<Node> nodes = new List<Node>();
        public Sequence(List<Node> nodes)
        {
            this.nodes = nodes;
        }
        public Sequence()
        {
            nodes = new List<Node>();
        }
        public override NodeState Evaluate()
        {
            bool isAnyNodeRunning = false;
            foreach (var node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        nodeState = NodeState.FAILURE;
                        return nodeState;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.RUNNING:
                        isAnyNodeRunning = true;
                        break;
                }
            }
            nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return nodeState;
        }
    }
}


