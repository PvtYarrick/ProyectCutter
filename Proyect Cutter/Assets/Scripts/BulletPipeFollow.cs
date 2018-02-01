using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPipeFollow : MonoBehaviour
{
    private float bulletSpeed = 0.06f;
    public float lifeTime = 2f;
    public int currentSegment = 0;
   
    public GameObject shotRenderer;
    

    private Pipe currentPipe = null;
    private Pipe currentPipeLinked = null;

    private PipeSystem system;
    private float previousDistance = float.MaxValue;

    private Quaternion childRot;
    private Transform rotaterChild;

    private Avatar avatar;

    void Start()
    {
        avatar = Avatar.instance;
    }

    public void SetInitialTarget(Pipe nextTargetPipe, int currentSegment, Quaternion nextRotation, PipeSystem _system)
    {
        currentPipe = nextTargetPipe;
        currentPipeLinked = currentPipe.linkedNextPipe;
        system = _system;

        this.currentSegment = currentSegment;
        GetNextDirection();

        rotaterChild = transform.GetChild(0);
        childRot = nextRotation;

        Destroy(gameObject, lifeTime);

        StartCoroutine(ActivateTrail());
    }
    IEnumerator ActivateTrail()
    {
        yield return new WaitForSeconds(0.09f);
        
      
        shotRenderer.SetActive(true);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f);
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
#endif

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed);
        Vector3 targetPos = currentPipe.segmentMatrix[currentSegment].position;
        Vector3 targetDir = targetPos - transform.position;

        Debug.DrawLine(targetPos, transform.position);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1000, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        float distance = Vector3.Magnitude(currentPipe.segmentMatrix[currentSegment].position - transform.position);

        if (distance > previousDistance)
        { 
            previousDistance = float.MaxValue;
            GetNextDirection();
        }
        else
        {
            previousDistance = distance;
        }
        rotaterChild.rotation = childRot;

        if (avatar.poweredUp == true)
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            Material mat = renderer.material;
            mat.SetColor("_EmissionColor", Color.blue);
        }

    }

    void GetNextDirection()
    {
        currentSegment++;
        if (currentSegment >= currentPipe.segmentMatrix.Count)
        {
            currentPipe = currentPipe.linkedNextPipe;
            currentSegment = 0;
        }

    }

    

    
}
