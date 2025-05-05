using Mirror;
using UnityEngine;

public class PlayerVisibilityManager : NetworkBehaviour
{
    public GameObject[] packageColors;
    public GameObject[] dropZoneColors;
    public GameObject[] packageGreys;
    public GameObject[] dropZoneGreys;

    void Start()
    {
        if (isLocalPlayer)
        {
            if (isServer) // Host
            {
                SetVisibility(packageColors, true);
                SetVisibility(packageGreys, false);
                SetVisibility(dropZoneColors, false);
                SetVisibility(dropZoneGreys, true);

                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("DropZoneColour"));
                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("PackageGrey"));
            }
            else // Client
            {
                SetVisibility(packageColors, false);
                SetVisibility(packageGreys, true);
                SetVisibility(dropZoneColors, true);
                SetVisibility(dropZoneGreys, false);

                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("PackageColour"));
                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("DropZoneGrey"));
            }
        }
    }

    void SetVisibility(GameObject[] objects, bool visible)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(visible);
        }
    }
}
