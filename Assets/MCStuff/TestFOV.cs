using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    public Transform pT;
    Transform target;

    public float meshResolution;
    public bool spotted;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;




    private void Start()
    {

        
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    IEnumerator WaitTime(float time, bool change)
    {
        yield return new WaitForSeconds(time);
        change = false;
    }

    private void Update()
    {
        pT = GameObject.FindGameObjectWithTag("Player").transform;
        DrawFieldOfView();
        //FindVisibleTargets();
        if(spotted)
        {
            //GetComponent<AIControl>().playerSpotted = true;
            //StartCoroutine(WaitTime(0.2f, spotted));
        }
        
    }

   


    void FindVisibleTargets()
    {
        visibleTargets.Clear(); //clears the list of any targets
        //ALL ENEMIES REQUIRE COLLIDERS FOR THIS TO WORK!!!!
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        //This Finds Enemies in the FOV Area- isnt visualized yet, but math wise its there
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            target = targetsInViewRadius[i].transform; //targets transform (player)
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            if(Vector2.Angle(transform.up, dirToTarget) < viewAngle /2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    Debug.Log("player spotted");
                    spotted = true;
                }
                else
                {
                    spotted = false;
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for(int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.z + (-viewAngle / 2 + stepAngleSize * i);
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(-angle, true) * viewRadius, Color.red);
            ViewCastInfo newViewCast = ViewCast(-angle);
            viewPoints.Add(newViewCast.point);

        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            
            if ( i < vertexCount -2) //math shiz
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
          
        }
        //this draws the FOV thing thats visible. 
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    private void LateUpdate()
    {
        if(spotted)
        {
            Debug.Log("Player is being detected! " + spotted + " .");
        }
        //if (visibleTargets.Contains(target))
        //{
        //    if (Vector3.Distance(target.position, pT.position) <= 0.2f)
        //    {
        //        spotted = true;
        //    }
        //    else spotted = false;

        //}
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
        
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }



}
