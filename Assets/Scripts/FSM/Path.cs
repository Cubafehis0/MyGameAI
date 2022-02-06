using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyFSM
{
    public class Path : MonoBehaviour
    {
        List<Vector3> points=null;
        float[] pathValue=null;
        float pathLength=0;
        readonly float maxValueOffset = 0.02f;
        public float PathLength { get => pathLength; }
        int PointCnt { get => points == null ? 0 : points.Count; }
        void Start()
        {
            var transforms = new List<Transform>(GetComponentsInChildren<Transform>());
            transforms.Remove(transforms[0]);
            points = transforms.Select(t => t.position).ToList();
            foreach (var p in points) Debug.Log(p);
            for (int i = 0; i < PointCnt - 1; i++)
                pathLength += (points[i + 1] - points[i]).magnitude;
            pathValue = new float[PointCnt];
            for (int i = 1; i < PointCnt; i++)
                pathValue[i] = pathValue[i - 1] + (points[i] - points[i - 1]).magnitude / pathLength;
        }
        public float GetParam(Vector3 position, float lastParam)
        {
            while (lastParam < 0) lastParam++;
            while (lastParam >= 1) lastParam--;
            float result = lastParam;
            Vector3 p1, p2;
            float maxDis = 10000000;
            float[] distances = new float[PointCnt];
            float[] values = new float[PointCnt];
            for (int i = 0; i < PointCnt; i++)
            {
                p1 = points[i];
                p2 = points[(i + 1)%PointCnt];
                distances[i] = Distance(position, p1, p2);
                float projectionSize = ProjectionSize(position, p1, p2) / pathLength;
                if (projectionSize < 0)
                    values[i] = pathValue[i];
                else
                    values[i] = pathValue[i] + projectionSize;
            }
            float min = maxDis;
            int index = -1;

            for (int i = 0; i < PointCnt; i++)
            {
                if (distances[i] < min && values[i] >= lastParam && values[i] <= lastParam + maxValueOffset)
                {
                    min = distances[i];
                    index = i;
                }
            }
            if (index >= 0)
                result = values[index];
            //为找到对应路径参数时，返回路径首部
            return result;
        }
        public float GetParam(Vector3 position)
        {
            float result = 0;
            Vector3 p1, p2;
            float maxDis = 10000000;
            float[] distances = new float[PointCnt];
            float[] values = new float[PointCnt];
            for (int i = 0; i < PointCnt; i++)
            {
                p1 = points[i];
                p2 = points[(i + 1) % PointCnt];
                distances[i] = Distance(position, p1, p2);
                float projectionSize = ProjectionSize(position, p1, p2) / pathLength;
                if (projectionSize < 0)
                    values[i] = pathValue[i];
                else
                    values[i] = pathValue[i] + projectionSize;
            }
            float min = maxDis;
            int index = -1;

            for (int i = 0; i < PointCnt; i++)
            {
                if (distances[i] < min)
                {
                    min = distances[i];
                    index = i;
                }
            }
            if (index >= 0)
                result = values[index];
            //为找到对应路径参数时，返回路径首部
            return result;
        }
        public Vector3 GetPosition(float param)
        {
            while (param < 0) param++;
            while (param >= 1) param--;
            for (int i = 0; i < pathValue.Length; i++)
            {
                if (param >= pathValue[i] && param <= pathValue[i + 1])
                {
                    float ratio = (param - pathValue[i]) / (pathValue[i + 1] - pathValue[i]);
                    return points[i + 1] * ratio + points[i] * (1 - ratio);
                }
            }
            return Vector3.zero;
        }
        public Vector3 GetReturnPos(Vector3 origin)
        {
            Vector3 target=Vector3.zero;
            bool s = false;
            for (int i = 0; i < PointCnt; i++)
            {
                if(ProjectionSize(origin,points[i],points[(i+1)%PointCnt])>0)
                {
                    Vector3 v = GetPedal(origin, points[i], points[(i + 1) % PointCnt]);
                    if(!s)
                    {
                        target = v;
                        s = true;
                    }
                    else
                        if (Vector3.Distance(v, origin) < Vector3.Distance(v, target))
                            target = v;
                }
            }
            return target;
        }

        float ProjectionSize(Vector3 point, Vector3 left, Vector3 right)
        {
            Vector3 v = point - left;
            Vector3 line = (right - left).normalized;
            return Vector3.Dot(v, line);
        }
        Vector3 GetPedal(Vector3 point,Vector3 left,Vector3 right)
        {
            float x0= point[0], y0 = point[1], z0 = point[2];
            float x1 = left[0], y1 = left[1], z1 = left[2];
            float x2 = right[0], y2 = right[1], z2 = right[2];
            float k = -((x1 - x0) * (x2 - x1) + (y1 - y0) * (y2 - y1) + (z1 - z0) * (z2 - z1)) / (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1);
            return new Vector3(k*(x2 - x1) + x1, k*(y2 - y1) + y1, k * (z2 - z1) + z1);
        }
        float Distance(Vector3 point, Vector3 left, Vector3 right)
        {
            Vector3 v = point - left;
            Vector3 line = (right - left).normalized;
            return Vector3.Cross(v, line).magnitude;
        }
    }
}