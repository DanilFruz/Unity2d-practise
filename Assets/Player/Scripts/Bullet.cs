using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float force;
    public float freq;
    Collider2D col;
    Rigidbody2D rgbd;
    Animator animator;
    private void Awake()
    {
        animator  = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    private void Start()
    {
        transform.right = direction;
    }
    private void FixedUpdate()
    {
        
        rgbd.AddForce(direction * force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(destroy());
    }
    
    private IEnumerator destroy()
    {
        
        animator.Play("Hit");
        rgbd.velocity = direction * 1f;
        force = 0;
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }


}
