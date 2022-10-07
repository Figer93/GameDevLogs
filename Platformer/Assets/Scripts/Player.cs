using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Statistic")]
    [Range(0, 10f)] public float _speed = 1f;
    [Range(0, 15f)] public float _jumpForce = 8f;
    public float _health = 1f;

    [Space]
    [Header("Ground Checker Settings")]
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] [Range(-5, 5f)] private float _checkGroundOffsetY = -1.8f;
    [SerializeField] [Range(0, 5f)] private float _checkGroundRadius = 0.3f;

    [Header("Animator Settings")]
    [SerializeField] private Animator _anim;

    private Rigidbody2D _rb;
    private float _moveInput;
    private bool facingRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal") * _speed;

        _anim.SetFloat("HorizontalMove", Mathf.Abs(_moveInput));

        if (!facingRight && _moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && _moveInput < 0)
        {
            Flip();
        }
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
        if (_isGrounded == false)
        {
            _anim.SetBool("isjumping", true);
        }
        else
        {
            _anim.SetBool("isjumping", false); // анимация

        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_moveInput * 10f, _rb.velocity.y);
        _rb.velocity = targetVelocity;
        CheckGround();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + _checkGroundOffsetY), _checkGroundRadius);

        if (colliders.Length > 1)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
}
