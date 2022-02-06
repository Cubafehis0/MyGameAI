using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MyBt
{
    public class ShootNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAI ai;

        public ShootNode(NavMeshAgent agent, EnemyAI ai)
        {
            this.agent = agent;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("ShootNode Evaluate");
            agent.isStopped = true;
            ai.SetColor(Color.green);
            return NodeState.RUNNING;
        }
    }

}
