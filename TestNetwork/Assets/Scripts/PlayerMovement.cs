using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 2f;
    private Animator goblinCont;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        goblinCont = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 playMovement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += playMovement;

     
        bool isMoving = moveX != 0 || moveY != 0;

        if (goblinCont != null)
        {
            goblinCont.SetBool("isWalking", isMoving);
        }

        if (spriteRenderer != null && moveX != 0)
        {
            spriteRenderer.flipX = moveX < 0;
        }
    }
}
