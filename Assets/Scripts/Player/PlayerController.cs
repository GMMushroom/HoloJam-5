using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f; //TODO:Remove [SerializeField] after setting up a suitable value.
    [SerializeField] private float _jumpForce = 6f; //TODO:Remove [SerializeField] after setting up a suitable value.
    private float _groundCheckRadius = 0.1f;
    private float _jumpCooldown = 0.02f;
    
    private int _jumpCount;
    private int _maxJumps = 2;
    
    private bool _isGrounded;
    
    [SerializeField] private Transform _groundCheck;

    [SerializeField] private LayerMask _groundLayer;
    
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);

        bool wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        if (_isGrounded && !wasGrounded)
        {
            StartCoroutine(ResetJumpsWithDelay());
        }

        if (Input.GetButtonDown("Jump") && _jumpCount < _maxJumps)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpCount++;
        }
    }

    IEnumerator ResetJumpsWithDelay()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _jumpCount = 0;
    }
}