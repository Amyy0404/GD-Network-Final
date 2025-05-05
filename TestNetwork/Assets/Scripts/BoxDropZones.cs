using UnityEngine;
using Mirror;

public class BoxDropZone : NetworkBehaviour
{
    public string acceptedColor; // "Red", "Blue", or "Yellow"

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isServer) return;

        if (other.CompareTag("Package"))
        {
            PackageInfo info = other.GetComponent<PackageInfo>();
            // Inside OnTriggerEnter2D...
            if (info != null && info.color == acceptedColor)
            {
                Debug.Log("CORRECT PACKAGE DELIVERED!");
                NetworkServer.Destroy(other.gameObject);
                ScoreManager.instance.AddScore(10); // Add points
            }

        }
    }
}