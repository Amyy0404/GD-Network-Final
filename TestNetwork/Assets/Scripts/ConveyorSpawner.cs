using Mirror;
using UnityEngine;

public class ConveyorSpawner : NetworkBehaviour
{
    public GameObject redPrefab;
    public GameObject bluePrefab;
    public GameObject yellowPrefab;

    public GameObject redGreyPrefab;
    public GameObject blueGreyPrefab;
    public GameObject yellowGreyPrefab;

    public ConveyorSegment startingSegment;

    public float spawnInterval = 2f;
    private float timer = 0f;

    void Update()
    {
        if (!isServer) return; // Only the server controls spawning

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        int rand = Random.Range(0, 3);
        GameObject colorPrefab;
        GameObject greyPrefab;

        if (rand == 0)
        {
            colorPrefab = redPrefab;
            greyPrefab = redGreyPrefab;
        }
        else if (rand == 1)
        {
            colorPrefab = bluePrefab;
            greyPrefab = blueGreyPrefab;
        }
        else
        {
            colorPrefab = yellowPrefab;
            greyPrefab = yellowGreyPrefab;
        }

        // Spawn the color version
        GameObject colorInstance = Instantiate(colorPrefab, startingSegment.spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(colorInstance);
        colorInstance.GetComponent<ConveyorMover>().StartMoving(startingSegment);

        // Spawn the grey version
        GameObject greyInstance = Instantiate(greyPrefab, startingSegment.spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(greyInstance);
        greyInstance.GetComponent<ConveyorMover>().StartMoving(startingSegment);

        // Make grey follow color
        greyInstance.GetComponent<PackageVisualFollower>().target = colorInstance.transform;

    }
}

