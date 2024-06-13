using UnityEngine;

public class CameraControlelr : MonoBehaviour
{
    public Transform player;

    public Vector3 Offset;

    void Update()
    {
        transform.position = player.position + Offset;
    }
}
