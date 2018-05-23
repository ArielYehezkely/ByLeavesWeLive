using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Timeline;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mewlist.BSplinePath
{
    public class Path : MonoBehaviour
    {
        public struct Point
        {
            public Vector3 Position;
            public Vector3 Direction;
            public Point(Vector3 position, Vector3 direction)
            {
                Position = position;
                Direction = direction;
            }
        }

        struct Section
        {
            public float Distance;
            public Vector3 Direction;
            public Section(float distance, Vector3 direction)
            {
                Distance = distance;
                Direction = direction;
            }
        }

        private const int resolution = 15;

        [Range(0f, 1f)]
        public float Rate = 0f;
        public List<Vector3> Points = new List<Vector3>();

        private int steps;
        private float length;
        private List<Section> sections;

        public Point GetPoint(float t)
        {
            if (sections == null) CalcSections();
            if (Points.Count == 1) return new Point(Lerp(0f), Vector3.forward);
            if (steps <= 0) return new Point(Vector3.zero, Vector3.zero);
            var d = t * length;
            var i = sections.FindIndex(v => v.Distance > 0 && v.Distance >= d);
            var step = Mathf.Max(1, i);
            var from = sections[step - 1];
            var to = sections[step];
            var tInSection = (d - from.Distance) / (to.Distance - from.Distance);
            var tt = ((float)step + tInSection) / (float)(steps + 1);
            return new Point(Lerp(tt), Vector3.Lerp(from.Direction, to.Direction, tInSection));
        }

        public Vector3 Lerp(float t)
        {
            var tm = Points.Count;
            var scaledT = t * tm;
            var i = Mathf.Clamp(Mathf.FloorToInt(scaledT), 0, tm - 1);
            var p1 = i > 0 ? Points[i - 1] : Points[0];
            var p2 = Points[i];
            var p3 = i < tm - 1 ? Points[i + 1] : Points[tm - 1];

            return transform.localPosition + p1 * BasisFunc2(i - 2, scaledT) +
                p2 * BasisFunc2(i - 1, scaledT) +
                p3 * BasisFunc2(i, scaledT);
        }

        public void ClearCache()
        {
            sections = null;
            length = 0f;
            steps = 0;
        }

        private void OnValidate() { ClearCache(); }

        private float BasisFunc2(int i, float t)
        {
            t = (t - i);
            if (0f <= t && t < 1f) return 0.5f * t * t;
            if (1f <= t && t < 2f) return -t * t + 3f * t - 3f / 2f;
            if (2f <= t && t < 3f) return 0.5f * (t - 3f) * (t - 3f);
            return 0;
        }

        private void CalcSections()
        {
            sections = new List<Section>();
            if (Points.Count == 0) return;

            steps = resolution * (Points.Count - 1);
            var prevPoint = Points[0] + transform.localPosition;
            sections.Add(new Section(0f, Vector3.zero));
            for (int i = 1; i <= steps; i++)
            {
                var t = (float)i / (float)steps;
                var p = Lerp(t);
                var direction = p - prevPoint;
                sections.Add(new Section(
                    sections[i - 1].Distance + (p - prevPoint).magnitude,
                    direction.normalized));
                prevPoint = p;
            }
            length = sections.Last().Distance;
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            var point = GetPoint(Rate);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.TransformPoint(point.Position), 1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.TransformPoint(point.Position),
                 transform.TransformPoint(point.Position) + 3f * point.Direction);
        }

        [MenuItem("GameObject/Create Other/BSplinePath")]
        public static void CreateGameObject()
        {
            GameObject activeGameObject = Selection.activeGameObject;
            GameObject path = new GameObject();
            path.name = "Spline Path";
            path.AddComponent<Path>();
            if (activeGameObject != null)
            {
                path.transform.SetParent(activeGameObject.transform, false);
            }
        }
#endif
    }
}