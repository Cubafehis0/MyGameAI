using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{
    public class IsCoverNode : Node
    {
        private Transform target;
        private Transform origin;

        public IsCoverNode(Transform target, Transform origin)
        {
            this.target = target;
            this.origin = origin;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("IsCoverNode Evaluate");
            RaycastHit hit;
            if(Physics.Raycast(origin.position,target.position-origin.position,out hit))
            {
                if(hit.collider.transform!=target)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;

        }
    }

}
