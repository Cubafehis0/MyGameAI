using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MyBt
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float startingHealth;
        [SerializeField] private float lowHealthThreshold;
        [SerializeField] private float healthRestoreRate;

        [SerializeField] private float chasingRange;
        [SerializeField] private float shootingRange;

        [SerializeField] Transform player;
        [SerializeField] private Cover[] avaliableCovers;

        [SerializeField] private float currentHealth;
        private Material material;
        private Transform bestCoverSpot;
        private NavMeshAgent agent;

        private Node topNode;
        public float CurrentHealth { get => currentHealth; set => currentHealth = Mathf.Clamp(value, 0, startingHealth); }

        public Transform BestCoverSpot { set => bestCoverSpot = value; get => bestCoverSpot; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            material = GetComponent<MeshRenderer>().material;
        }
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = startingHealth;
            ConstructBehaviourTree();
        }

        private void ConstructBehaviourTree()
        {    
            ChaseNode chaseNode = new ChaseNode(player, agent, this);
            RangeNode chasingRangeNode = new RangeNode(chasingRange, player, transform);
            Sequence chaseSequence = new Sequence(new List<Node>() { chasingRangeNode, chaseNode });

            //RangeNode shootingRangeNode = new RangeNode(shootingRange, player,transform);
            //ShootNode shootNode = new ShootNode(agent, this);
            //Sequence shootSequence = new Sequence(new List<Node>() { shootingRangeNode, shootNode });

            HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
            IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(avaliableCovers, player, this);
            GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
            Sequence goToCoverSequence = new Sequence(new List<Node>() { coverAvaliableNode, goToCoverNode });
            Selector findCoverSelector = new Selector(new List<Node>() { goToCoverSequence, chaseNode });
            IsCoverNode isCoverNode = new IsCoverNode(player, transform);
            Selector tryToTakeCoverSelector= new Selector(new List<Node>() {isCoverNode,findCoverSelector });
            Sequence coverSequence = new Sequence(new List<Node>() { healthNode, tryToTakeCoverSelector });

            topNode = new Selector(new List<Node>() { coverSequence,chaseSequence });
        }

        // Update is called once per frame
        void Update()
        {
            topNode.Evaluate();
            if (topNode.NodeState== NodeState.FAILURE)
                SetColor(Color.red);
            Debug.Log(string.Format("Distance:{0}", Vector3.Distance(transform.position, player.position)));
            CurrentHealth += Time.deltaTime * healthRestoreRate;
        }
        private void OnMouseDown()
        {
            Debug.Log("MouseDown");
            CurrentHealth -= 10f;
        }
        public void SetColor(Color color)
        {
            material.color = color;
        }

    }

}
