using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    public float speed = 1.5f;
    private ConveyorSegment currentSegment;
    private Transform targetPoint;

    public void StartMoving(ConveyorSegment startSegment)
    {
        currentSegment = startSegment;
        targetPoint = currentSegment.exitPoint;
    }

    void Update()
    {
        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            if (currentSegment.nextSegments.Count > 0)
            {
                int rand = Random.Range(0, currentSegment.nextSegments.Count);
                currentSegment = currentSegment.nextSegments[rand];
                targetPoint = currentSegment.exitPoint;
            }
            else
            {
                Destroy(gameObject); // End of belt
            }
        }
    }
}
