using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBt
{

    public class IsCoverAvaliableNode : Node
    {
        private Cover[] avaliableCovers;
        private Transform target;
        private EnemyAI ai;

        public IsCoverAvaliableNode(Cover[] avaliableCovers, Transform target, EnemyAI ai)
        {
            this.avaliableCovers = avaliableCovers;
            this.target = target;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("IsCoverAvaliableNode Evaluate");
            Transform bestSpot = FindBestCoverSpot();
            ai.BestCoverSpot=bestSpot;
            return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
        }

        private Transform FindBestCoverSpot()
        {
            float minAngle = 90;
            Transform bestSpot = null;
            foreach(var cover in avaliableCovers)
            {
                Transform bestSpotInCover = FindBestSpotInCover(cover,ref minAngle);
                if (bestSpotInCover != null)
                    bestSpot = bestSpotInCover;
            }
            return bestSpot;
        }

        private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
        {
            Transform[] avaliableSpots = cover.CoverSpots;
            Transform bestSpot = null;
            for (int i = 0; i < avaliableSpots.Length; i++)
            {
                Vector3 direction = target.position - avaliableSpots[i].position;
                if (CheckIfCoverIsValid(avaliableSpots[i]))
                {
                    float angle = Vector3.Angle(avaliableSpots[i].forward, direction);
                    if(angle<minAngle)
                    {
                        minAngle = angle;
                        bestSpot = avaliableSpots[i];
                    }
                }
            }
            return bestSpot;
        }
        //玩家不能看到spot即为有效    
        private bool CheckIfCoverIsValid(Transform spot)
        {
            RaycastHit hit;
            Vector3 direction = target.position - spot.position;
            if(Physics.Raycast(spot.position,direction,out hit))
            {
                if (hit.collider.transform != target)
                    return true;
            }
            return false;
        }
    }

}
