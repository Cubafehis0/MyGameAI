using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFSM
{
    public class PatrolState : State
    {
        Path path;
        Transform origin;
        Transform target;
        float speed;
        float currentParam;
        float alertDis;
        public PatrolState(StateMachine machine, Path path, Transform origin,Transform target, float speed,float alertDis) :base(machine)
        {
            this.path = path;
            this.origin = origin;
            this.target = target;
            this.speed = speed;
            this.alertDis = alertDis;
            type = StateType.Patrol;
        }

        public override void CheckTransition()
        {
            if (Vector3.Distance(origin.position, target.position) < alertDis)
            {
                stateMachine.Transition(StateType.Chase);
                return;
            }
        }

        public override void Enter()
        {
            Debug.Log("Patrol State Enter");
            currentParam = path.GetParam(origin.position);
        }

        public override void Excute()
        {
            Debug.Log("Patrol State Excute");
            currentParam = path.GetParam(origin.position, currentParam);
            Debug.Log(currentParam);
            float l = path.PathLength;
            Vector3 tar = path.GetPosition(currentParam + speed*Time.deltaTime*2/l);   
            Debug.Log(tar);
            Vector3 s = (tar - origin.position).normalized * speed;
            origin.Translate(s * Time.deltaTime);
        }
        public override void Exit()
        {
            Debug.Log("Patrol State Exit");
        }
    }
}
