using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalSpeed;
    private Animator animator;
    private InputManager inputManager;
    private CharacterController characterController;

    private const float gravity = 9.8f;

    public static PlayerController instance;
    public LayerMask floorLayerMask;
    public float speed;
    public float jumpSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        PauseMenuController.OnGamePaused += PauseCamera;
        GameOverMenuController.OnGamePaused += PauseCamera;
    }

    private void OnDestroy()
    {
        PauseMenuController.OnGamePaused -= PauseCamera;
        GameOverMenuController.OnGamePaused -= PauseCamera;
    }

    private void FixedUpdate()
    {
        var moveDirection = new Vector2(inputManager.Horizontal, inputManager.Vertical);

        Move(moveDirection);

        Animate(moveDirection);
    }

    void Move(Vector2 direction)
    {
        animator.SetFloat("VerticalSpeed", verticalSpeed);

        var moveDirection = transform.TransformDirection(new Vector3(direction.x, 0, direction.y));
        moveDirection *= speed;
        moveDirection.y = verticalSpeed;
        moveDirection *= Time.deltaTime;

        characterController.Move(moveDirection);


        if (Grounded())
        {
            verticalSpeed = 0f;

            if (inputManager.SpaceKey)
            {
                verticalSpeed = jumpSpeed;
            }
        }

        verticalSpeed -= gravity * Time.deltaTime;
    }

    void Animate(Vector2 direction)
    {
        animator.SetFloat("VelocityX", direction.x);
        animator.SetFloat("VelocityY", direction.y);
    }

    private bool Grounded()
    {
        return characterController.isGrounded;
    }

    private void PauseCamera(bool paused)
    {
        instance.GetComponentInChildren<ThirdPersonCameraController>().enabled = !paused;
    }
}
