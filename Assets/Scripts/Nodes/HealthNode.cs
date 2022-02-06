using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    public class HealthNode : Node
    {
        private EnemyAI ai;
        private float threshold;

        public HealthNode(EnemyAI ai, float threshold)
        {
            this.ai = ai;
            this.threshold = threshold;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("HealthNode Evaluate");
            return ai.CurrentHealth<=threshold?NodeState.SUCCESS:NodeState.FAILURE;
        }
    }

}
