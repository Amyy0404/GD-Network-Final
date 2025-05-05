using UnityEngine;

public class PackageVisualFollower : MonoBehaviour
{
    public Transform target; // This will be the colored package

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
