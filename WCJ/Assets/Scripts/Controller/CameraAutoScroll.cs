using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private GameObject player;

    private void Update()
    {
        // Nueva posición X: se incrementa por scrollSpeed.
        float newX = transform.position.x + scrollSpeed * Time.deltaTime;
        float newY = player.transform.position.y;
        float newZ = transform.position.z;
        transform.position = new Vector3(newX, newY, newZ);
    }
}
