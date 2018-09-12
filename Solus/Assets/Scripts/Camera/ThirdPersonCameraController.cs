using System;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    [Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    [SerializeField] MouseInput mouseControl;

    public Transform camVerticalAxis;
    public Transform player;
    public float cameraDistance;
    public float speed;
    public float minXAngle;
    public float maxXAngle;
    public LayerMask layerMask;

    private InputManager inputManager;
    private float currentXAngle;

    public void Awake()
    {
        inputManager = GetComponentInParent<InputManager>();
    }

    public void Update()
    {
        var mouseInput = new Vector2();

        mouseInput.x = Mathf.Lerp(mouseInput.x, inputManager.MouseInput.x * mouseControl.Sensitivity.x, 1f / mouseControl.Damping.x);
        player.transform.Rotate(0, mouseInput.x, 0);

        currentXAngle += Mathf.Lerp(mouseInput.y, inputManager.MouseInput.y * mouseControl.Sensitivity.y, 1f / mouseControl.Damping.y);
        currentXAngle = Mathf.Clamp(currentXAngle, minXAngle, maxXAngle);

        camVerticalAxis.transform.localEulerAngles = new Vector3(-currentXAngle, 0, 0);
    }

    void LateUpdate()
    {
        RaycastHit hitInfo;

        transform.position = !Physics.Raycast(camVerticalAxis.transform.position, -camVerticalAxis.transform.forward, out hitInfo, cameraDistance, layerMask)
            ? Vector3.Lerp(transform.position, camVerticalAxis.transform.position - camVerticalAxis.transform.forward * cameraDistance, speed * Time.deltaTime)
            : hitInfo.point;
    }
}
