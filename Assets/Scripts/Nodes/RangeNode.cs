using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    //目标物体是否在敌人范围内，是SUCCESS 否返回FAILURE
    public class RangeNode : Node
    {
        private float range;
        private Transform origin;
        private Transform target;

        public RangeNode(float range, Transform origin, Transform target)
        {
            this.range = range;
            this.origin = origin;
            this.target = target;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("RangeNode Evaluate");
            return  Vector3.Distance(origin.position , target.position) <= range ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }

}
