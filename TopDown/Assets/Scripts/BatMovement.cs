using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _movement;
    private float _moveSpeed = 1.2f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _movement = _playerRigidbody.position - _rigidbody.position;
        _movement.Normalize();

        if (_movement.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_movement.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movement.x * _moveSpeed, _movement.y * _moveSpeed);
    }
}
