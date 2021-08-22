using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;
    
    [SerializeField] [Range(0f, .2f)] private float cameraHeight;
    [SerializeField] [Range(0.5f, 10f)] private float movingSpeed = 1.5f;
    
    private void FixedUpdate()
    {
        if (PlayerTransform == null) return;
        float cameraPositionY = transform.position.y + cameraHeight;
        Vector3 cameraPosition = new Vector3(transform.position.x, cameraPositionY, -10);
        Vector3 target = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, -10);

        Vector3 pos = Vector3.Lerp(cameraPosition, target, movingSpeed * Time.deltaTime);

        transform.position = pos;
    }
    
}