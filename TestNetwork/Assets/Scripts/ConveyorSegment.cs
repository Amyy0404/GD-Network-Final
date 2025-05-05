using System.Collections.Generic;
using UnityEngine;

public class ConveyorSegment : MonoBehaviour
{
    [Header("Assign the exit point in the scene")]
    public Transform exitPoint;

    [Header("Assign all possible connected conveyor segments")]
    public List<ConveyorSegment> nextSegments;

    public Transform spawnPoint;
}
