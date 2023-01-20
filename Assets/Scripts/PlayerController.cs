using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;

    private Alteruna.Avatar _avatar;
    private MeshRenderer _renderer;
    

   

    void Start()
    {
        
        
        // Get components
        _avatar = GetComponent<Alteruna.Avatar>();
        _renderer = GetComponent<MeshRenderer>();

        if (_avatar.IsMe)
        {
            _renderer.material.color = Color.green;
        }
        else
        {
            _renderer.material.color = Color.red;
        }
    }

    void Update()
    {
        // Only let input affect the avatar if it belongs to me
        if (_avatar.IsMe)
        {
            float _translation = Input.GetAxis("Vertical") * Speed;
            Vector3 dir = new Vector3(0, _translation, 0);
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 1))
            {
                if (hit.transform.GetComponent<WallController>())
                {
                    return;
                }
            }
            // Get the horizontal and vertical axis.
            _translation *= Time.deltaTime;
            transform.Translate(0, _translation, 0, Space.Self);
        }
    
    }
}