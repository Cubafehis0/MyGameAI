using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFSM
{
    public class ChaseState : State
    {
        Transform origin=null;
        Transform target=null;
        float speed=0f;
        float chaseDis;
        public ChaseState(StateMachine machine, Transform origin, Transform target, float speed, float chaseDis) : base(machine)
        {
            this.origin = origin;
            this.target = target;
            this.speed = speed;
            this.chaseDis = chaseDis;
            type = StateType.Chase;
        }

        public override void CheckTransition()
        {
            if (Vector3.Distance(target.position, origin.position) > chaseDis)
            {
                stateMachine.Transition(StateType.Patrol);
                return;
            }
        }

        public override void Enter()
        {
            Debug.Log("Chase State Enter");
        }

        public override void Excute()
        {
            Debug.Log("Chase State Excute");
            Vector3 dec = target.position - origin.position;
            Vector3 s = dec.normalized * speed;
            origin.Translate(s * Time.deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("Chase State Exit");
        }
    }
}
