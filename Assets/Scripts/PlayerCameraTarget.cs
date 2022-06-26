using Cinemachine;
using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    [Tooltip("The speed at which the local rotation returns to zero")]
    [SerializeField] private float cameraSnapSpeed;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private Vector3 scrollWheelOffset;

    private float _rotation;
    private Cinemachine3rdPersonFollow _camFollow;

    private void Awake() => _camFollow = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

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

        if (Input.mouseScrollDelta.y != 0)
        {
            _camFollow.ShoulderOffset += -scrollWheelOffset * Input.mouseScrollDelta.y;

            if (_camFollow.ShoulderOffset.y is < 2 or > 10)
                _camFollow.ShoulderOffset -= -scrollWheelOffset * Input.mouseScrollDelta.y;
        }
    }
}