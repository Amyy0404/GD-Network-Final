using Mirror;
using UnityEngine;

public class PlayerPackageHandler : NetworkBehaviour
{
    public Transform holdPoint;
    private GameObject heldPackage;

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldPackage == null)
            {
                TryPickup();
            }
            else
            {
                CmdDropPackage();
            }
        }
    }

    void TryPickup()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Package")) continue;

            if (hit.GetComponent<NetworkIdentity>() != null && hit.GetComponent<ConveyorMover>() != null)
            {
                CmdPickupPackage(hit.gameObject);
                break;
            }
        }
    }

    [Command]
    void CmdPickupPackage(GameObject package)
    {
        if (heldPackage != null || package == null) return;

        heldPackage = package;
        NetworkIdentity pkgId = package.GetComponent<NetworkIdentity>();

        // Always notify the player who picked it up
        TargetAttachPackage(connectionToClient, pkgId.netId);

        // Notify everyone else to show that this player picked it up
        RpcAttachPackage(pkgId.netId, netId);
    }

    [Command]
    void CmdDropPackage()
    {
        if (heldPackage == null) return;

        GameObject dropped = heldPackage;
        heldPackage = null;

        dropped.transform.SetParent(null);
        dropped.GetComponent<Rigidbody2D>().simulated = true;
        dropped.GetComponent<ConveyorMover>().enabled = false;

        RpcDropPackage(dropped.GetComponent<NetworkIdentity>().netId);
    }

    [TargetRpc]
    void TargetAttachPackage(NetworkConnection target, uint netId)
    {
        if (NetworkClient.spawned.TryGetValue(netId, out NetworkIdentity identity))
        {
            GameObject pkg = identity.gameObject;
            pkg.transform.SetParent(holdPoint);
            pkg.transform.localPosition = Vector3.zero;
            pkg.GetComponent<Rigidbody2D>().simulated = false;
            pkg.GetComponent<ConveyorMover>().enabled = false;
            heldPackage = pkg;
        }
    }

    [ClientRpc]
    void RpcAttachPackage(uint pkgNetId, uint playerNetId)
    {
        if (!NetworkClient.spawned.TryGetValue(pkgNetId, out NetworkIdentity pkgId)) return;
        if (!NetworkClient.spawned.TryGetValue(playerNetId, out NetworkIdentity playerId)) return;

        // Skip the player who already got the TargetRpc
        if (playerId.isLocalPlayer) return;

        GameObject pkg = pkgId.gameObject;
        GameObject playerObj = playerId.gameObject;
        PlayerPackageHandler handler = playerObj.GetComponent<PlayerPackageHandler>();

        pkg.transform.SetParent(handler.holdPoint);
        pkg.transform.localPosition = Vector3.zero;
        pkg.GetComponent<Rigidbody2D>().simulated = false;
        pkg.GetComponent<ConveyorMover>().enabled = false;

        handler.heldPackage = pkg;
    }

    [ClientRpc]
    void RpcDropPackage(uint pkgNetId)
    {
        if (NetworkClient.spawned.TryGetValue(pkgNetId, out NetworkIdentity pkgId))
        {
            GameObject pkg = pkgId.gameObject;

            pkg.transform.SetParent(null);
            pkg.GetComponent<Rigidbody2D>().simulated = true;
            pkg.GetComponent<ConveyorMover>().enabled = false;

            if (heldPackage == pkg)
                heldPackage = null;
        }
    }
}

