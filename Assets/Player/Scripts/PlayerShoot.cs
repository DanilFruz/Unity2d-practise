using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject bulletType;
    Bullet bullet;
    Animator animator;
    
    public float preCast = 0f;
    public Transform weaponPosition;
    private bool checkShoot = false;
    
    private bool _isShooting = false;
    public bool isShooting { get { return _isShooting; }set { _isShooting = value; animator.SetBool(AnimationStrings.isShooting, value); } }
    [SerializeField] private Transform mousePosition; 

     void Awake()
    {

        animator = GetComponent<Animator>();
        bullet = bulletType.GetComponent<Bullet>();
    }
    private void Update()
    {
        
        
        if (PlayerInput.Instance.GetFire1() > 0 && checkShoot == false)
        {
            StartCoroutine(Shoot());
            
        }
    }
    public IEnumerator Shoot()
    {
        checkShoot = true;
        isShooting = true;
        
        yield return new WaitForSeconds(preCast);
        bullet.direction = (mousePosition.transform.position - weaponPosition.position).normalized;
        Instantiate(bulletType, weaponPosition.position, bullet.transform.rotation);
        isShooting = false;
        yield return new WaitForSeconds(bullet.freq);
        checkShoot = false;


    }
}
