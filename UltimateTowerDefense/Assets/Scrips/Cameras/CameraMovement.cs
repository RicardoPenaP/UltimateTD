using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement")]
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
        MoveByEdgeScrollingInput();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void MoveByKeysInput()
    {
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

    private void MoveByEdgeScrollingInput()
    {
        GetMousePosition();
        MoveByEdgeSrolling();
    }
    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
    }
    private void MoveByEdgeSrolling()
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

    
}
