using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;   
void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player.position.x >= transform.position.x)
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
