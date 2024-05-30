using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private float _bulletSpeed = 3f;
    private float _bulletDuration = 2f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _movement = _playerRigidbody.position - _rigidbody.position;
        _movement.Normalize();

    }

    private void Update()
    {
        if(_bulletDuration <= 0)
        {
            Destroy(gameObject);
        } else
        {
            _bulletDuration -= Time.deltaTime;
        }
        
    }
    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movement.x * _bulletSpeed, _movement.y * _bulletSpeed);

    }
}
