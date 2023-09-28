using UnityEngine;
using Cinemachine;

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
    [Header("Camera Zoom")]
    [Tooltip("Max diference from the starting zoom")]
    [SerializeField, Min(0f)] private float maxZoom;
    [Tooltip("Zoom step by every mouse scroll")]
    [SerializeField, Min(0f)] private float zoomStep;
    [Tooltip("Zoom smooth speed")]
    [SerializeField, Min(0f)] private float zoomSmoothSpeed;


    private Transform myPointer;
    private PlayerInput playerInput;
    private CinemachineVirtualCamera virtualCamera;

    private Vector2 keysRawInput;
    private Vector2 mousePosition;
    private Vector2 mouseLastPos;

    private bool movementByDragActive = false;
    private float startingFOV;
    private float targetFOV;

    private void Awake()
    {
        myPointer = GetComponentInChildren<CameraPointer>().transform;
        playerInput = new PlayerInput();
        virtualCamera = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        startingFOV = virtualCamera.m_Lens.FieldOfView;
        targetFOV = virtualCamera.m_Lens.FieldOfView;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void Update()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }
        MoveByKeysInput();
        MoveByEdgeScrollInput();
        MoveByMouseDragInput();
        ZoomHandler();
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
        Vector3 rawMovementDirection = new Vector3(keysRawInput.x, 0, keysRawInput.y);
        MovePointer(rawMovementDirection, byKeysMovementSpeed);        
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

        Vector3 rawMovementDirection = new Vector3();

        if (mousePosition.x < 0 + horizontalTresholdInPixels)
        {
            rawMovementDirection.x = -1f;
        }

        if (mousePosition.x > Screen.width - horizontalTresholdInPixels)
        {
            rawMovementDirection.x = 1f;
        }

        if (mousePosition.y < 0 + verticalTresholdInPixels)
        {
            rawMovementDirection.z = -1f;
        }

        if (mousePosition.y > Screen.height - verticalTresholdInPixels)
        {
            rawMovementDirection.z = 1f;
        }

        MovePointer(rawMovementDirection,byEdgeScrollingMovementSpeed);
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

        Vector3 rawMovementDirection = new Vector3(mouseLastPos.x -currentMousePos.x , 0 ,mouseLastPos.y - currentMousePos.y);

        MovePointer(rawMovementDirection,byDragMouseMovementSpeed);
        mouseLastPos = playerInput.Player.MousePosition.ReadValue<Vector2>();
    }

    private void MovePointer(Vector3 rawMovementDirection, float movementSpeed)
    {
        Vector3 movementDirection = myPointer.position + rawMovementDirection * movementSpeed * Time.deltaTime;
        movementDirection.x = Mathf.Clamp(movementDirection.x, GridMananger.Instance.MyThresholds.minX, GridMananger.Instance.MyThresholds.maxX);
        movementDirection.z = Mathf.Clamp(movementDirection.z, GridMananger.Instance.MyThresholds.minZ, GridMananger.Instance.MyThresholds.maxZ);

        myPointer.position = movementDirection;
    }

    private void ZoomHandler()
    {
        float minValue = startingFOV - maxZoom;
        float maxValue = startingFOV + maxZoom;
        float zoomDirection = -Input.mouseScrollDelta.y;
        targetFOV += (zoomDirection * zoomStep);
        targetFOV = Mathf.Clamp(targetFOV, minValue, maxValue);
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, zoomSmoothSpeed * Time.deltaTime);        
    }
}
