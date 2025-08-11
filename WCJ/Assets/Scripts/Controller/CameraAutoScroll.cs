using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;//Initial scroll speed
    [SerializeField] private float speedIncreaseAmount = 0.5f;//Amount to increase speed by
    [SerializeField] private float maxScrollSpeed = 5f;//Maximum allowed scroll speed
    [SerializeField] private float firstSpeedIncreaseTime = 10f;//Time for the first speed increase
    [SerializeField] private float speedIncreaseInterval = 15f;//Time between subsequent speed increases
    [SerializeField] private float minY = 6f;//Minimum Y position for the camera
    [SerializeField] private float maxY = 35f;//Maximum Y position for the camera
    [SerializeField] private GameObject player;//Reference to the player
    private float nextSpeedIncreaseTime;//Time for the next speed increase
    private void Start()
    {
        // Schedule the first speed increase
        nextSpeedIncreaseTime = Time.time + firstSpeedIncreaseTime;
    }

    private void Update()
    {
        // Increase scroll speed if it's time and the max hasn't been reached
        if (Time.time >= nextSpeedIncreaseTime && scrollSpeed < maxScrollSpeed)
        {
            scrollSpeed = Mathf.Min(scrollSpeed + speedIncreaseAmount, maxScrollSpeed);
            nextSpeedIncreaseTime += speedIncreaseInterval;
        }
        // Move the camera forward on the X axis
        float newX = transform.position.x + scrollSpeed * Time.deltaTime;
        // Follow the player's Y position, clamped within minY and maxY
        float clampedY = Mathf.Clamp(player.transform.position.y, minY, maxY);
        // Set the new camera position
        transform.position = new Vector3(newX, clampedY, transform.position.z);
    }
}
