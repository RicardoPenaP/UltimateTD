using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputMananger : MonoBehaviour
{
    [Header("Player Input Mananger")]    
    [Header("Input Actions")]
    [SerializeField] private InputAction pauseInput = new InputAction();
    

    private void OnEnable()
    {
        pauseInput.Enable();
    }

    private void OnDisable()
    {
        pauseInput.Disable();
    }

    private void Update()
    {
        PauseMenuInput();
    }

    private void PauseMenuInput()
    {
        if (pauseInput.triggered)
        {
            BuildingMananger.Instance?.CloseBuildingManangerWindow();
            TerrainMananger.Instance?.CloseTerrainMananger();
            PauseMenu.Instance?.ToggleMenu();
        }
    }
}
