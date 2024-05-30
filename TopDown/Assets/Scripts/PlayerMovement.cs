using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector2 _movement;
    private float _moveSpeed;
    private float _runningSpeed = 2f;
    private bool _canDash = true;
    private bool _isDashing = false;
    private float _dashingPower = 8f;
    private float _dashingDuration = 0.2f;
    private float _dashingCooldown = 4f;

    //bool dash = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _moveSpeed = _runningSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if(!_isDashing)
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _movement.Normalize();

            _animator.SetFloat("speed", _rigidbody.velocity.magnitude);

            if (_movement.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_movement.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
        
        if (Input.GetButtonDown("Jump") && _canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movement.x * _moveSpeed, _movement.y * _moveSpeed);
    }

    private IEnumerator Dash()
    {
        _animator.SetBool("isDashing", true);
        _canDash = false;
        _isDashing = true;
        _moveSpeed = _dashingPower;
        yield return new WaitForSeconds(_dashingDuration);
        _animator.SetBool("isDashing", false);
        _isDashing = false;
        _moveSpeed = _runningSpeed;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}
