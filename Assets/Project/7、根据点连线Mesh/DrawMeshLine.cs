using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawMeshLine : MonoBehaviour
{
    public struct CurveSegment2D
    {
        public Vector2 point1;
        public Vector2 point2;

        public CurveSegment2D(Vector2 point1, Vector2 point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public Vector2 SegmentVector
        {
            get
            {
                return point2 - point1;
            }
        }
    }

    [HideInInspector]
    public List<Vector2> nodeList = new List<Vector2>();
    [SerializeField]
    public List<GameObject> GameobjectList = new List<GameObject>();
    public int smooth = 5;
    public float width = 0.2f;

    private Mesh _mesh;

    
    private void Start()
    {
        BuildMesh();
    }

    void Init()
    {
        foreach (GameObject go in GameobjectList)
        {
            Vector2 v2 = new Vector2(go.transform.position.x, go.transform.position.z);
            nodeList.Add(v2);
        }
        if (_mesh == null)
        {
            _mesh = new Mesh();
            _mesh.name = "CurveMesh";
            GetComponent<MeshFilter>().mesh = _mesh;
        }
    }
    public bool BuildMesh()
    {
        Init();
        _mesh.Clear();
        if (nodeList.Count < 2)
        {
            return false;
        }
        List<Vector2> curvePoints = CalculateCurve(nodeList, smooth, false);
        List<Vector2> vertices = GetVertices(curvePoints, width * 0.5f);
        //List<Vector2> verticesUV = GetVerticesUV(curvePoints);

        Vector3[] _vertices = new Vector3[vertices.Count];
        //Vector2[] _uv = new Vector2[verticesUV.Count];
        int[] _triangles = new int[(vertices.Count - 2) * 3];
        for (int i = 0; i < vertices.Count; i++)
        {
            _vertices[i].Set(vertices[i].x, 0, vertices[i].y);
        }
        //for (int i = 0; i < verticesUV.Count; i++)
        //{
        //    _uv[i].Set(verticesUV[i].x, verticesUV[i].y);
        //}
        for (int i = 2; i < vertices.Count; i += 2)
        {
            int index = (i - 2) * 3;
            _triangles[index] = i - 2;
            _triangles[index + 1] = i - 0;
            _triangles[index + 2] = i - 1;
            _triangles[index + 3] = i - 1;
            _triangles[index + 4] = i - 0;
            _triangles[index + 5] = i + 1;
        }
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        //_mesh.uv = _uv;
        //_mesh.RecalculateNormals();

        return true;
    }

    /// <summary>
    /// Calculate Catmul-Rom Curve
    /// </summary>
    /// <param name="points">key points</param>
    /// <param name="smooth">how many segments between two nearby point</param>
    /// <param name="curveClose">whether curve is a circle</param>
    /// <returns></returns>
    public List<Vector2> CalculateCurve(IList<Vector2> points, int smooth, bool curveClose)
    {
        int pointCount = points.Count;
        int segmentCount = curveClose ? pointCount : pointCount - 1;

        List<Vector2> allVertices = new List<Vector2>((smooth + 1) * segmentCount);
        Vector2[] tempVertices = new Vector2[smooth + 1];
        float smoothReciprocal = 1f / smooth;

        for (int i = 0; i < segmentCount; ++i)
        {
            // get 4 adjacent point in points to calculate position between p1 and p2
            Vector2 p0, p1, p2, p3;
            p1 = points[i];

            if (curveClose)
            {
                p0 = i == 0 ? points[segmentCount - 1] : points[i - 1];
                p2 = i + 1 < pointCount ? points[i + 1] : points[i + 1 - pointCount];
                p3 = i + 2 < pointCount ? points[i + 2] : points[i + 2 - pointCount];
            }
            else
            {
                p0 = i == 0 ? p1 : points[i - 1];
                p2 = points[i + 1];
                p3 = i == segmentCount - 1 ? p2 : points[i + 2];
            }

            Vector2 pA = p1;
            Vector2 pB = 0.5f * (-p0 + p2);
            Vector2 pC = p0 - 2.5f * p1 + 2f * p2 - 0.5f * p3;
            Vector2 pD = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);

            float t = 0;
            for (int j = 0; j <= smooth; j++)
            {
                tempVertices[j] = pA + t * (pB + t * (pC + t * pD));
                t += smoothReciprocal;
            }
            for (int j = allVertices.Count == 0 ? 0 : 1; j < tempVertices.Length; j++)
            {
                allVertices.Add(tempVertices[j]);
            }
        }
        return allVertices;
    }

    private List<CurveSegment2D> GetSegments(List<Vector2> points)
    {
        List<CurveSegment2D> segments = new List<CurveSegment2D>(points.Count - 1);
        for (int i = 1; i < points.Count; i++)
        {
            segments.Add(new CurveSegment2D(points[i - 1], points[i]));
        }
        return segments;
    }

    private List<Vector2> GetVertices(List<Vector2> points, float expands)
    {
        List<CurveSegment2D> segments = GetSegments(points);

        List<CurveSegment2D> segments1 = new List<CurveSegment2D>(segments.Count);
        List<CurveSegment2D> segments2 = new List<CurveSegment2D>(segments.Count);

        for (int i = 0; i < segments.Count; i++)
        {
            Vector2 vOffset = new Vector2(-segments[i].SegmentVector.y, segments[i].SegmentVector.x).normalized;
            segments1.Add(new CurveSegment2D(segments[i].point1 + vOffset * expands, segments[i].point2 + vOffset * expands));
            segments2.Add(new CurveSegment2D(segments[i].point1 - vOffset * expands, segments[i].point2 - vOffset * expands));
        }

        List<Vector2> points1 = new List<Vector2>(points.Count);
        List<Vector2> points2 = new List<Vector2>(points.Count);

        for (int i = 0; i < segments1.Count; i++)
        {
            if (i == 0)
            {
                points1.Add(segments1[0].point1);
            }
            else
            {
                Vector2 crossPoint;
                if (!TryCalculateLinesIntersection(segments1[i - 1], segments1[i], out crossPoint, 0.1f))
                {
                    crossPoint = segments1[i].point1;
                }
                points1.Add(crossPoint);
            }
            if (i == segments1.Count - 1)
            {
                points1.Add(segments1[i].point2);
            }
        }
        for (int i = 0; i < segments2.Count; i++)
        {
            if (i == 0)
            {
                points2.Add(segments2[0].point1);
            }
            else
            {
                Vector2 crossPoint;
                if (!TryCalculateLinesIntersection(segments2[i - 1], segments2[i], out crossPoint, 0.1f))
                {
                    crossPoint = segments2[i].point1;
                }
                points2.Add(crossPoint);
            }
            if (i == segments2.Count - 1)
            {
                points2.Add(segments2[i].point2);
            }
        }

        List<Vector2> combinePoints = new List<Vector2>(points.Count * 2);
        for (int i = 0; i < points.Count; i++)
        {
            combinePoints.Add(points1[i]);
            combinePoints.Add(points2[i]);
        }
        return combinePoints;
    }

    //private List<Vector2> GetVerticesUV(List<Vector2> points)
    //{
    //    List<Vector2> uvs = new List<Vector2>(points.Count * 2);
    //    float totalLength = 0;
    //    float totalLengthReciprocal = 0;
    //    float curLength = 0;
    //    for (int i = 1; i < points.Count; i++)
    //    {
    //        totalLength += Vector2.Distance(points[i - 1], points[i]);
    //    }
    //    totalLengthReciprocal = uvTiling / totalLength;
    //    for (int i = 0; i < points.Count; i++)
    //    {
    //        if (i == 0)
    //        {
    //            uvs.Add(new Vector2(0, 1));
    //            uvs.Add(new Vector2(0, 0));
    //        }
    //        else
    //        {
    //            if (i == points.Count - 1)
    //            {
    //                uvs.Add(new Vector2(uvTiling, 1));
    //                uvs.Add(new Vector2(uvTiling, 0));
    //            }
    //            else
    //            {
    //                curLength += Vector2.Distance(points[i - 1], points[i]);
    //                float uvx = curLength * totalLengthReciprocal;

    //                uvs.Add(new Vector2(uvx, 1));
    //                uvs.Add(new Vector2(uvx, 0));
    //            }
    //        }
    //    }
    //    return uvs;
    //}

    private bool TryCalculateLinesIntersection(CurveSegment2D segment1, CurveSegment2D segment2, out Vector2 intersection, float angleLimit)
    {
        intersection = new Vector2();

        Vector2 p1 = segment1.point1;
        Vector2 p2 = segment1.point2;
        Vector2 p3 = segment2.point1;
        Vector2 p4 = segment2.point2;

        float denominator = (p2.y - p1.y) * (p4.x - p3.x) - (p1.x - p2.x) * (p3.y - p4.y);
        // If denominator is 0, means parallel
        if (denominator == 0)
        {
            return false;
        }

        // Check angle between segments
        float angle = Vector2.Angle(segment1.SegmentVector, segment2.SegmentVector);
        // if the angle between two segments is too small, we treat them as parallel
        if (angle < angleLimit || (180f - angle) < angleLimit)
        {
            return false;
        }

        float x = ((p2.x - p1.x) * (p4.x - p3.x) * (p3.y - p1.y)
                + (p2.y - p1.y) * (p4.x - p3.x) * p1.x
                - (p4.y - p3.y) * (p2.x - p1.x) * p3.x) / denominator;
        float y = -((p2.y - p1.y) * (p4.y - p3.y) * (p3.x - p1.x)
                + (p2.x - p1.x) * (p4.y - p3.y) * p1.y
                - (p4.x - p3.x) * (p2.y - p1.y) * p3.y) / denominator;

        intersection.Set(x, y);
        return true;
    }

}
