using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;

    void Awake()
    {
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        transform.position = player.position + offset;
    }

}
