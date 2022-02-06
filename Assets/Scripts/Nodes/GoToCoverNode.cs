using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MyBt
{
    public class GoToCoverNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAI ai;

        public GoToCoverNode(NavMeshAgent agent, EnemyAI ai)
        {
            this.agent = agent;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("GoToCoverNode Evaluate");
            Debug.LogError(ai.BestCoverSpot);
            Transform spot = ai.BestCoverSpot;
            if (spot == null)
                return NodeState.FAILURE;
            ai.SetColor(Color.blue);
            float distance = Vector3.Distance(spot.position, agent.transform.position);
            if(distance>0.2f)
            {
                agent.isStopped = false;
                agent.SetDestination(spot.position);
                return NodeState.RUNNING;
            }
            else
            {
                agent.isStopped = true;
                return NodeState.SUCCESS ;
            }
        }
        
    }

}