using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform boyTransform;
    public Transform bearTransform;
    public Transform cameraTarget;

    void Update()
    {
        if (boyTransform != null && bearTransform != null && cameraTarget != null)
        {
            // Calculate the midpoint between the boy and the bear
            Vector3 midpoint = (boyTransform.position + bearTransform.position) / 2f;
            cameraTarget.position = midpoint;
        }
    }
}