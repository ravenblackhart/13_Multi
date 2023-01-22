using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float Speed = 10.0f;

    private Alteruna.Avatar _avatar;
    private SpriteRenderer _renderer;
    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    void Start()
    {
        // Get components
        _avatar = GetComponent<Alteruna.Avatar>();
        _renderer = GetComponent<SpriteRenderer>();

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
            // Set the avatar representing me to be green
            _renderer.color = Color.green;
            _uiManager.RecolorScores(_avatar);

            // Get the horizontal and vertical axis.
            float _translation = Input.GetAxis("Vertical") * Speed;
            _translation *= Time.deltaTime;

            Vector2 pos = transform.position;
            Vector2 direction = new Vector2(0, _translation);
            
            RaycastHit2D[] result = new RaycastHit2D[5];
            int numberOfHits = Physics2D.RaycastNonAlloc(pos, direction, result, 4f);
        
            if (numberOfHits > 0) {
                foreach (RaycastHit2D hit in result) {
                    if (!hit ) {
                        break;
                    }
                    
                    WallController wall = hit.transform.GetComponent<WallController>();
                    if (wall) { return; }
                }
            }
            
            transform.Translate(0, _translation, 0, Space.Self);
        }
    }
}