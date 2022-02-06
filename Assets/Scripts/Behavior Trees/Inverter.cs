using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    public class Inverter : Node
    {
        public Node node;
        public Inverter(Node node)
        {
            this.node = node;
        }
        public override NodeState Evaluate()
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    nodeState = NodeState.SUCCESS;
                    break;
                case NodeState.SUCCESS:
                    nodeState = NodeState.FAILURE;
                    break;
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    break;
            }
            return nodeState;
        }
    }

}
