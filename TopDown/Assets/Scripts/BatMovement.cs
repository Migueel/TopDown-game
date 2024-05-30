using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _movement;
    private float _moveSpeed = 0.8f;
    private bool _isDashing = false;
    private float _dashingPower = 4f;
    private float _dashingDuration = 0.5f;
    private float _flySpeed = 0.1f;
    private float _dashingCooldown = 5f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Dash());
    }

    void Update()
    {
        if (!_isDashing)
        {
            _movement = _playerRigidbody.position - _rigidbody.position;
            _movement.Normalize();

        }

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

    private IEnumerator Dash()
    {
        yield return new WaitForSeconds(_dashingCooldown);
        _isDashing = true;
        _moveSpeed = _dashingPower;
        yield return new WaitForSeconds(_dashingDuration);
        _isDashing = false;
        _moveSpeed = _flySpeed;
        StartCoroutine(Dash());

    }
}
