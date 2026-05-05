using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float moveSpeed;
    private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, 1.0f, player.position.z) + offset, moveSpeed);
    }

    public void SetSpeed(float playerSpeed)
    {
        moveSpeed = playerSpeed;
    }
}
