using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class BoxDropZone : NetworkBehaviour
{
    public string acceptedColor; // "Red", "Blue", "Yellow"

    [SerializeField] private List<Transform> possibleSpawnPoints; // Assign in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isServer) return;

        if (other.CompareTag("Package"))
        {
            PackageInfo info = other.GetComponent<PackageInfo>();
            if (info != null && info.color == acceptedColor)
            {
                Debug.Log("CORRECT PACKAGE DELIVERED!");
                NetworkServer.Destroy(other.gameObject); // remove from game

                MoveDropZoneToNewPosition();
            }
            else
            {
                Debug.Log("WRONG PACKAGE!");
                // Optional: destroy or leave the package
            }
        }
    }

    [Server]
    void MoveDropZoneToNewPosition()
    {
        if (possibleSpawnPoints == null || possibleSpawnPoints.Count == 0) return;

        int randomIndex = Random.Range(0, possibleSpawnPoints.Count);
        Vector3 newPosition = possibleSpawnPoints[randomIndex].position;

        transform.position = newPosition;

        // Optional: visual feedback
        RpcFlashDropZone();
    }

    [ClientRpc]
    void RpcFlashDropZone()
    {
        // Optional: add visual feedback for movement
        Debug.Log("Drop zone moved!");
    }
}
