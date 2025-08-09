using UnityEngine;
namespace Runner
{
    public class CameraAutoScroll : MonoBehaviour
    {
        //Speed for the camera movement
        [SerializeField] private float scrollSpeed = 3f;
        private void Update()
        {
            transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}
