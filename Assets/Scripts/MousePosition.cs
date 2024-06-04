using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 mouseInput;

    private void Awake()
    {
        
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        mouseInput = PlayerInput.Instance.GetMouseAxis();
        transform.position = mouseInput;
    }
}
