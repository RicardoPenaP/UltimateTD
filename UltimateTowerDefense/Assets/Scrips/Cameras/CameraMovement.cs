using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement")]
    [Header("Movement Methods Active")]
    [SerializeField] private bool byKeyInput = false;
    [SerializeField] private bool byEdgeScroll = false;
    [SerializeField] private bool byMouseDrag = false;
    [Header("Movement Speed Settings")]
    [SerializeField, Min(0f)] private float byKeysMovementSpeed = 10f;
    [SerializeField, Min(0f)] private float byEdgeScrollingMovementSpeed = 10f;
    [SerializeField, Min(0f)] private float byDragMouseMovementSpeed = 10f;
    [Header("Treshold Settings")]
    [Tooltip("Percentage of screen height")]
    [SerializeField, Range(0, 50)] private int verticalTreshold = 10;
    [Tooltip("Percentage of screen width")]
    [SerializeField, Range(0, 50)] private int horizontalTreshold = 10;

    private Transform myPointer;
    private PlayerInput playerInput;

    private Vector2 keysRawInput;
    private Vector2 mousePosition;
    private Vector2 mouseLastPos;

    private bool movementByDragActive = false;

    private void Awake()
    {
        myPointer = GetComponentInChildren<CameraPointer>().transform;
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void Update()
    {
        MoveByKeysInput();
        MoveByEdgeScrollInput();
        MoveByMouseDragInput();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void MoveByKeysInput()
    {
        if (!byKeyInput)
        {
            return;
        }
        GetKeysRawInput();
        MoveByKeys();
    }
    private void GetKeysRawInput()
    {
        keysRawInput = playerInput.Player.Move.ReadValue<Vector2>();
    }
    private void MoveByKeys()
    {
        Vector3 movementDirection = new Vector3(keysRawInput.x, 0, keysRawInput.y);
        myPointer.position += movementDirection * byKeysMovementSpeed * Time.deltaTime;
    }

    private void MoveByEdgeScrollInput()
    {
        if (!byEdgeScroll)
        {
            return;
        }
        GetMousePosition();
        MoveByEdgeSroll();
    }
    private void GetMousePosition()
    {
        mousePosition = playerInput.Player.MousePosition.ReadValue<Vector2>();
    }
    private void MoveByEdgeSroll()
    {
        int horizontalTresholdInPixels = (Screen.width * horizontalTreshold) / 100;
        int verticalTresholdInPixels = (Screen.height * verticalTreshold) / 100;

        Vector3 movementDirection = new Vector3();

        if (mousePosition.x < 0 + horizontalTresholdInPixels)
        {
            movementDirection.x = -1f;
        }

        if (mousePosition.x > Screen.width - horizontalTresholdInPixels)
        {
            movementDirection.x = 1f;
        }

        if (mousePosition.y < 0 + verticalTresholdInPixels)
        {
            movementDirection.z = -1f;
        }

        if (mousePosition.y > Screen.height - verticalTresholdInPixels)
        {
            movementDirection.z = 1f;
        }

        myPointer.position += movementDirection * byEdgeScrollingMovementSpeed * Time.deltaTime;
    }

    private void MoveByMouseDragInput()
    {
        if (!byMouseDrag)
        {
            return;
        }
        GetMouseLastPosition();
        MoveByDrag();
    }
    private void GetMouseLastPosition()
    {
        if (playerInput.Player.MouseRightButton.WasPressedThisFrame())
        {
            movementByDragActive = true;
            mouseLastPos = playerInput.Player.MousePosition.ReadValue<Vector2>();
        }

        if (playerInput.Player.MouseRightButton.WasReleasedThisFrame())
        {
            movementByDragActive = false;
        }
    }

    private void MoveByDrag()
    {
        if (!movementByDragActive)
        {
            return;
        }
        Vector2 currentMousePos = playerInput.Player.MousePosition.ReadValue<Vector2>();

        Vector3 movementDirection = new Vector3(mouseLastPos.x -currentMousePos.x , 0 ,mouseLastPos.y - currentMousePos.y);

        myPointer.position += movementDirection * byDragMouseMovementSpeed * Time.deltaTime;
        mouseLastPos = playerInput.Player.MousePosition.ReadValue<Vector2>();
    }

}
