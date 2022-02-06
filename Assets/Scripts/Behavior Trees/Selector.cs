using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    public class Selector : Node
    {
        public List<Node> nodes = new List<Node>();
        public Selector(List<Node> nodes)
        {
            this.nodes = nodes;
        }
        public Selector()
        {
            nodes = new List<Node>();
        }
        public override NodeState Evaluate()
        {
            foreach (var node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        break;
                    case NodeState.SUCCESS:
                        nodeState = NodeState.SUCCESS;
                        return nodeState;
                    case NodeState.RUNNING:
                        nodeState = NodeState.RUNNING;
                        return nodeState;
                }
            }
            nodeState = NodeState.FAILURE;

            return nodeState;
        }
    }

}
