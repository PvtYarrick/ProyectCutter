using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public float pipeRadius;
	public int pipeSegmentCount;

	public float ringDistance;

	public float minCurveRadius, maxCurveRadius;
	public int minCurveSegmentCount, maxCurveSegmentCount;

	public PipeItemGenerator[] generators;

	private float curveRadius;
	private int curveSegmentCount;

	private Mesh mesh;
	private Vector3[] vertices;
	private Vector2[] uv;
	private int[] triangles;

	private float curveAngle;
	private float relativeRotation;

    //center location
    public List<Vector3> segmentPositions = new List<Vector3>();
    private List<GameObject> segmentCenterGameObjects = new List<GameObject>();
    public List<Transform> segmentMatrix = new List<Transform>();
    private List<Vector3> parentRelativePosition = new List<Vector3>();


    public Pipe linkedNextPipe;

	public float CurveAngle {
		get {
			return curveAngle;
		}
	}

	public float CurveRadius {
		get {
			return curveRadius;
		}
	}

	public float RelativeRotation {
		get {
			return relativeRotation;
		}
	}

	public int CurveSegmentCount {
		get {
			return curveSegmentCount;
		}
	}

	private void Awake () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Pipe";
	}

	public void Generate (bool withItems = true)
    {
        for(int i = 0; i < segmentCenterGameObjects.Count; i++)
        {
            DestroyImmediate(segmentCenterGameObjects[i]);
        }

        segmentCenterGameObjects.Clear();
        segmentPositions.Clear();
        segmentMatrix.Clear();
        parentRelativePosition.Clear();

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;


        curveRadius = Random.Range(minCurveRadius, maxCurveRadius);
		curveSegmentCount =
			Random.Range(minCurveSegmentCount, maxCurveSegmentCount + 1);
		mesh.Clear();
		SetVertices();
		SetUV();
		SetTriangles();
		mesh.RecalculateNormals();

		for (int i = 0; i < transform.childCount; i++)
        {
            if(!transform.GetChild(i).name.Equals("uCenter"))
			    Destroy(transform.GetChild(i).gameObject);
		}
		if (withItems) {
			generators[Random.Range(0, generators.Length)].GenerateItems(this);
		}

        for(int i = 0; i < segmentMatrix.Count; i++)
        {
            if (!mesh.bounds.Contains(segmentMatrix[i].position))
            {
                segmentCenterGameObjects.RemoveAt(i);
                segmentPositions.RemoveAt(i);
                parentRelativePosition.RemoveAt(i);
                segmentMatrix.RemoveAt(i);
                i--;
            }
        }
    }

	private void SetVertices () {
		vertices = new Vector3[pipeSegmentCount * curveSegmentCount * 4];

		float uStep = ringDistance / curveRadius;
		curveAngle = uStep * curveSegmentCount * (360f / (2f * Mathf.PI));
		CreateFirstQuadRing(uStep);
		int iDelta = pipeSegmentCount * 4;
		for (int u = 2, i = iDelta; u <= curveSegmentCount; u++, i += iDelta) {
			CreateQuadRing(u * uStep, i);
		}
		mesh.vertices = vertices;
	}

    private void CreateFirstQuadRing (float u)
    {
        float uStep = (2f * Mathf.PI) / curveSegmentCount;
        float vStep = (2f * Mathf.PI) / pipeSegmentCount;

		Vector3 vertexA = GetPointOnTorus(0f, 0f);
		Vector3 vertexB = GetPointOnTorus(u, 0f);


        Vector3 center = Vector3.zero;
        center.x = curveRadius * Mathf.Sin(u * uStep);
        center.y = curveRadius * Mathf.Cos(u * uStep);

        GameObject newCenter = new GameObject("uCenter");
        segmentCenterGameObjects.Add(newCenter);

        Transform transf = newCenter.transform;
        segmentMatrix.Add(transf);

        segmentPositions.Add(center);

        transf.position = center;
        transf.SetParent(this.transform, true);
        parentRelativePosition.Add(transf.localPosition);
       // transf.SetParent(null, false);


        for (int v = 1, i = 0; v <= pipeSegmentCount; v++, i += 4) {
			vertices[i] = vertexA;
			vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep);
			vertices[i + 2] = vertexB;
			vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep);
		}
	}

	private void CreateQuadRing (float u, int i) {
        float uStep = (2f * Mathf.PI) / curveSegmentCount;

        float vStep = (2f * Mathf.PI) / pipeSegmentCount;
		int ringOffset = pipeSegmentCount * 4;

		Vector3 vertex = GetPointOnTorus(u, 0f);


        Vector3 center = Vector3.zero;
        center.x = curveRadius * Mathf.Sin(u * uStep);
        center.y = curveRadius * Mathf.Cos(u * uStep);

        GameObject newCenter = new GameObject("uCenter");
        segmentCenterGameObjects.Add(newCenter);

        Transform transf = newCenter.transform;
        segmentMatrix.Add(transf);

        segmentPositions.Add(center);

        transf.position = center;
        transf.SetParent(this.transform, true);
        parentRelativePosition.Add(transf.localPosition);
        //transf.SetParent(null, false);


        for (int v = 1; v <= pipeSegmentCount; v++, i += 4)
        {
			vertices[i] = vertices[i - ringOffset + 2];
			vertices[i + 1] = vertices[i - ringOffset + 3];
			vertices[i + 2] = vertex;
			vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
		}
	}

	private void SetUV () {
		uv = new Vector2[vertices.Length];
		for (int i = 0; i < vertices.Length; i+= 4) {
			uv[i] = Vector2.zero;
			uv[i + 1] = Vector2.right;
			uv[i + 2] = Vector2.up;
			uv[i + 3] = Vector2.one;
		}
		mesh.uv = uv;
	}
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i = 0; i < segmentMatrix.Count; i++)
        {
            if (segmentMatrix[i] != null)
                Gizmos.DrawSphere(segmentMatrix[i].position, 0.1f);
            else
                Debug.LogError("there's a null sphere, with id " + i);
        }
    }
#endif

    private void SetTriangles () {
		triangles = new int[pipeSegmentCount * curveSegmentCount * 6];
		for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4) {
			triangles[t] = i;
			triangles[t + 1] = triangles[t + 4] = i + 2;
			triangles[t + 2] = triangles[t + 3] = i + 1;
			triangles[t + 5] = i + 3;
		}
		mesh.triangles = triangles;
	}

	private Vector3 GetPointOnTorus (float u, float v) {
		Vector3 p;
		float r = (curveRadius + pipeRadius * Mathf.Cos(v));
		p.x = r * Mathf.Sin(u);
		p.y = r * Mathf.Cos(u);
		p.z = pipeRadius * Mathf.Sin(v);
		return p;
	}

    public void AlignWith(Pipe pipe)
    {
        relativeRotation =
            Random.Range(0, curveSegmentCount) * 360f / pipeSegmentCount;

        transform.SetParent(pipe.transform, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -pipe.curveAngle);
        transform.Translate(0f, pipe.curveRadius, 0f);
        transform.Rotate(relativeRotation, 0f, 0f);
        transform.Translate(0f, -curveRadius, 0f);
        transform.SetParent(pipe.transform.parent);
        transform.localScale = Vector3.one;
    }

    public int getClosestSegment(Vector3 fromPosition)
    {
        int segmentID = 0;
        float closest = -1;
        for(int i = 0; i < segmentMatrix.Count; i++)
        {
            if(closest == -1 || Vector3.Magnitude(fromPosition - segmentMatrix[i].position) < closest)
            {
                closest = Vector3.Magnitude(fromPosition - segmentMatrix[i].position);
                segmentID = i;
            }
        }
        return segmentID;
    }
}