
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothness = 0.125f;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPos= target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothness);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

}
