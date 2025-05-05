using Mirror;
using UnityEngine;

public class EndPoint : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer) return;

        if (collision.CompareTag("ColorPrefab"))
        {
            NetworkServer.Destroy(collision.gameObject);
            HealthManager.instance.ReduceHealth(10);
        }
    }
}
