using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPipeFollow : MonoBehaviour
{
    public float bulletSpeed = 0.05f;

    public int currentSegment = 0;
    public Pipe currentPipe = null;
    public Pipe currentPipeLinked = null;
    private PipeSystem system;
    private float previousDistance = float.MaxValue;

    public void SetInitialTarget(Pipe nextTargetPipe, int currentSegment, Quaternion nextRotation, PipeSystem _system)
    {
        currentPipe = nextTargetPipe;
        currentPipeLinked = currentPipe.linkedNextPipe;
        system = _system;

        this.currentSegment = currentSegment;

        transform.GetChild(0).transform.rotation = nextRotation;

        getNextDirection();

    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f);
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
#endif
    void Update()
    {

        transform.Translate(Vector3.forward * bulletSpeed);


        if (currentPipe == null || currentSegment >= currentPipe.segmentPositions.Count)
        {
            Debug.LogError("We're out of segments!! Searching new");
            getNextDirection();
        }

        Vector3 targetPos = currentPipe.segmentMatrix[currentSegment].position;
        Vector3 targetDir = targetPos - transform.position;

        Debug.DrawLine(targetPos, transform.position);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1000, 0.0F);
       // Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
        Debug.Log("Distance " + Vector3.Magnitude(currentPipe.segmentPositions[currentSegment] - transform.position));

        float distance = Vector3.Magnitude(currentPipe.segmentPositions[currentSegment] - transform.position);
        if (distance > previousDistance)
        {
            previousDistance = float.MaxValue;
            getNextDirection();
        }
        else
        {
            previousDistance = distance;
        }

    }

    void getNextDirection()
    {
        if(currentPipe == null)
        {
            currentPipe = currentPipeLinked;
            currentPipeLinked = currentPipe.linkedNextPipe;
            currentSegment = 0;
        }
        else if (currentSegment >= currentPipe.segmentPositions.Count - 1)
        {
            if (currentPipe.linkedNextPipe == null)
            {
                currentPipe = system.getClosestPipe(transform.position);
                if (currentPipe.linkedNextPipe != null)
                    currentPipeLinked = currentPipe.linkedNextPipe;
                currentSegment = 0;
            }
            else
            {
                currentPipe = currentPipe.linkedNextPipe;
                currentPipeLinked = currentPipe.linkedNextPipe;
                currentSegment = 0;
            }
            
        }
        else
        {
            currentSegment++;
        }

    }
}
