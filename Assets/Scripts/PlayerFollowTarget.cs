using UnityEngine;

public class PlayerFollowTarget : MonoBehaviour
{
    [Tooltip("The speed at which the local rotation returns to zero")]
    [SerializeField] private float cameraSnapSpeed;
    
    private float _rotation;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") * 0.8f, 0f));
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero), cameraSnapSpeed * Time.deltaTime);
        }
    }
}