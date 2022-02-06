using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFSM
{
    public class EnemyAI : MonoBehaviour
    {
        private Transform origin=null;
        [SerializeField] Path path=null;
        [SerializeField] private Transform target=null;
        [SerializeField] private float alertDis=0;
        [SerializeField] private float speed=0;
        StateMachine stateMachine=null;

        private void Start()
        {
            origin = GetComponent<Transform>();
            ConstructStateMachine();
        }

        private void ConstructStateMachine()
        {
            stateMachine = new StateMachine();
            PatrolState patrolState=new PatrolState(stateMachine,path, origin,target, speed,alertDis);
            ChaseState chaseState = new ChaseState(stateMachine, origin, target, speed,alertDis);
            stateMachine.AddState(patrolState);
            stateMachine.AddState(chaseState);
            stateMachine.SetInitalState(StateType.Patrol);
        }

        private void Update()
        {
            stateMachine.Excute();
        }
    }

}