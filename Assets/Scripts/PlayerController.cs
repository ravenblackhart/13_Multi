using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float RotationSpeed = 180.0f;

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
            // Get the horizontal and vertical axis.
            float _translation = Input.GetAxis("Vertical") * Speed;
            float _rotation = -Input.GetAxis("Horizontal") * RotationSpeed;

            _translation *= Time.deltaTime;
            _rotation *= Time.deltaTime;

            transform.Translate(0, _translation, 0, Space.Self);
            transform.Rotate(0, 0, _rotation);
        }
    
    }
}