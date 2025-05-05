using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 2f;

    void Update()
    {
        if (isLocalPlayer)
        {
            // Move locally
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 playMovement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
            transform.position += playMovement;
        }
    }
}