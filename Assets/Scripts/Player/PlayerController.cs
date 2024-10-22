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

    private List<Buff> _activeBuffs = new();
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ModifySpeed(float amount)
    {
        _moveSpeed += amount;
        Debug.Log($"Spped: {_moveSpeed}");
    }

    public void ApplyBuff(Buff buff)
    {
        Buff existingBuff = _activeBuffs.Find(b => b.type == buff.type);
        if (existingBuff != null)
        {
            existingBuff.Remove();
            _activeBuffs.Remove(existingBuff);
        }
        
        _activeBuffs.Add(buff);
        buff.Apply();
    }
    
    void Update()
    {
        #region Movement

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

        #endregion

        for (int i = _activeBuffs.Count - 1; i >= 0; i--)
        {
            if (_activeBuffs[i].UpdateBuff(Time.deltaTime))
            {
                _activeBuffs[i].Remove();
                _activeBuffs.RemoveAt(i);
            }
        }
    }

    IEnumerator ResetJumpsWithDelay()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _jumpCount = 0;
    }
}