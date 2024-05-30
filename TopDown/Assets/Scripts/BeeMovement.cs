using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector2 _movement;
    private float _moveSpeed;
    private float _flySpeed = 1f;
    private float _angleToPlayer;
    private Vector3 _directionToPlayer;
    private bool _isAttacking;
    private float _attackSpeed = 0f;
    private float _attackDuration = 2f;
    private float _attackCooldown = 5f;
    private float _bulletCooldown = 0.7f;

    [SerializeField]
    private GameObject _bulletPrefab;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _moveSpeed = _flySpeed;
        StartCoroutine(Attack());
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

        // Calcula la dirección desde el objeto actual al objetivo
        _directionToPlayer = _playerRigidbody.position - _rigidbody.position;
        // Calcula el ángulo en grados (+90º para que coincida el sprite)
        _angleToPlayer = Mathf.Atan2(_directionToPlayer.y, _directionToPlayer.x) * Mathf.Rad2Deg + 90;

    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movement.x * _moveSpeed, _movement.y * _moveSpeed);
        _rigidbody.rotation = _angleToPlayer;
    }
    IEnumerator GenerateBullet()
    {
        while (_isAttacking)
        {
            Instantiate(_bulletPrefab, _rigidbody.position, Quaternion.Euler(new Vector3(0, 0, _angleToPlayer)));
            yield return new WaitForSeconds(_bulletCooldown);
        }

    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _animator.SetBool("isAttacking", true);
        _isAttacking = true;
        _moveSpeed = _attackSpeed;
        StartCoroutine(GenerateBullet());
        yield return new WaitForSeconds(_attackDuration);
        _animator.SetBool("isAttacking", false);
        _isAttacking = false;
        _moveSpeed = _flySpeed;
        StartCoroutine(Attack());
    }
}
