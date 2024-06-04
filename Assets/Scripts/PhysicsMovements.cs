using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class PhysicsMovements : MonoBehaviour
    {
    GameObject curObject;
    Animator animator;
    Ray2D ray;
    ContactFilter2D contactFilter;
    RaycastHit2D[] raycastHits = new RaycastHit2D[3];
    
    Rigidbody2D rgbd;
    private bool _isLookingUp = false;
    private Collider2D curCollider;
    public bool isLookingUp { get { return _isLookingUp; } set { _isLookingUp = value; animator.SetBool(AnimationStrings.isLookingUp, value); } }
    private bool _isOnWall = false;
    private bool isOnWall { get { return _isOnWall; } set { _isOnWall = value; } }
    private bool _isWalk = false;
    public bool isWalk
    {
        get
        {
            return _isWalk;
        }
        set
        {

            animator.SetBool(AnimationStrings.isMove, value);
            _isWalk = value;
        }
    }
    private bool _isJump = false;
    private int currentDirection = 1;
    public bool isJump { get { return _isJump; } set { _isJump = value; animator.SetBool(AnimationStrings.isJump, value); } }
    private bool _isGrounded = true;
    public bool isGrounded { get { return _isGrounded; } set { 
            _isGrounded = value; 
        } }

    public PhysicsMovements(GameObject gameObject)
    {
        this.curObject = gameObject;
        animator = gameObject.GetComponent<Animator>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        curCollider = gameObject.GetComponent<Collider2D>();
    }
    public void Jump(float jumpForce)
    {
         if (isGrounded && !isJump)
        {
            rgbd.AddForce(jumpForce * Vector2.up );
            isJump = true;
            
        }
        
        
        
    }
    public void Move( float dirSpeed)
    {
        
        if (isOnWall && dirSpeed > 0 && currentDirection > 0)
        {
            dirSpeed = 0;
        }
        else if(isOnWall && dirSpeed < 0 && currentDirection < 0)
        {
            dirSpeed = 0;
        }
        if(dirSpeed > 0 )
        {
            currentDirection = 1;
        }
        else if(dirSpeed < 0)
        {
            currentDirection = -1;
        }
        rgbd.velocity = new Vector2(dirSpeed, rgbd.velocity.y);
        isWalk = dirSpeed != 0;

        

    }
    public void ChangeDirection(float dir)
    {
        if((curObject.transform.localScale.x > 0 && dir < 0) || (curObject.transform.localScale.x < 0 && dir > 0))
        {

            curObject.transform.localScale = new Vector3(curObject.transform.localScale.x * -1, curObject.transform.localScale.y, curObject.transform.localScale.z);
        }

        
    }

    public void Checks()
    {

        isOnWall = Physics2D.BoxCast(curCollider.bounds.center, new Vector2(curCollider.bounds.size.x, curCollider.bounds.size.y - 0.3f) , 0f, Vector2.right * currentDirection,0.02f, 3);


        isGrounded = rgbd.Cast(Vector2.down, contactFilter,raycastHits,  0.03f) > 0;
        if (isJump && isGrounded) {
            isJump = false;
        }

    }


}