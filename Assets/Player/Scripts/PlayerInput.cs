using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Camera currentCamera;
    PlayerInputActions playerInputActions;
    static public PlayerInput Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;    
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    public Vector2 GetMovementAxis()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>();

    }
    public Vector2 GetMouseAxis()
    {

        return currentCamera.ScreenToWorldPoint(playerInputActions.Mouse.Mouse.ReadValue<Vector2>()); 
    }
    public float GetFire1()
    {
        return playerInputActions.Shoot.Fire1.ReadValue<float>();
    }
}
