using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 50f;
    PhysicsMovements Player;
    [SerializeField] private float mouseDirectionsOffsetX = 1f;
    [SerializeField] private Transform mousePositionObject;
    Animator animator;
    PlayerShoot playerShoot;
    Vector2 mousePosition;
    private bool _isLookingUp = false;
    [SerializeField] private float mouseDirectionsOffsetY = 0;
    public bool isLookingUp { get { return _isLookingUp; } set { _isLookingUp = value; animator.SetBool(AnimationStrings.isLookingUp, value); } }
    private Vector2 playerInput = Vector2.zero;
    private PlayerInputActions playerInputActions;
    float dir = 1;
    Vector2 mouseDir = Vector2.zero;

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        animator = GetComponent<Animator>();
        Player = new PhysicsMovements(this.gameObject);

    }
    void Start()
    {
        
    }
    void Update()
    {

        Player.Checks();
        playerInput = PlayerInput.Instance.GetMovementAxis();
        mousePosition =  mousePositionObject.transform.position;
        mouseDir = mousePosition - Vector2.one * transform.position;
        if(mousePosition.y > transform.position.y + mouseDirectionsOffsetY)
        {
            isLookingUp = true;
        }
        else
        {
            isLookingUp = false;
        }
        float mouseDirX = Mathf.Abs(mouseDir.x) >= mouseDirectionsOffsetX ? mouseDir.x : 0;
        if (PlayerInput.Instance.GetFire1() > 0)
        {
            Player.ChangeDirection(mouseDirX);
        }
        
        Player.ChangeDirection(playerInput.x);
        
        
    }
    private void FixedUpdate()
    {

        if (playerShoot.isShooting)
        {
            playerInput.x *= 0.5f;
        }
        Player.Move(speed * playerInput.x);
        if(playerInput.y > 0)
        {
            Player.Jump(jumpForce);
        }
        
    }
    
    
    




}
