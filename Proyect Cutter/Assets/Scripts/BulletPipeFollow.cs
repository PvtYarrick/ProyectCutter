using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPipeFollow : MonoBehaviour
{
    public float bulletSpeed = 0.05f;

    public int currentSegment = 0;
    public Pipe currentPipe = null;

	public void SetInitialTarget(Pipe nextTargetPipe, int currentSegment, Quaternion nextRotation)
    {
        currentPipe = nextTargetPipe;
        this.currentSegment = currentSegment;

        transform.rotation = nextRotation;
        getNextDirection();

    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed);

        if (currentSegment >= currentPipe.segmentPositions.Count)
            return;

        if (Vector3.Magnitude(currentPipe.segmentPositions[currentSegment] - transform.position) < 4.35f)
        {
            getNextDirection();
        }

    }

    void getNextDirection()
    {
        if (currentSegment >= currentPipe.segmentPositions.Count - 1)
        {
            currentPipe = currentPipe.linkedNextPipe;
            currentSegment = 0;
        }
        else
        {
            currentSegment++;
        }

        Vector3 targetPos = currentPipe.segmentPositions[currentSegment];
        Vector3 targetDir = targetPos - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1000, 0.0F);

        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
