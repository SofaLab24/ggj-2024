using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TriangleMarozController : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField] private Sprite groundedSprite;
    [SerializeField] private Sprite airborneSprite;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float moveSpeed = 10f;

    private float _moveDir;
    private bool isDrunk;
    public float Points;
    private bool  _jumpPressed;
    private float _jumpYVel;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _moveVel;
    private bool IsGrounded;
    private float drunkDuration = 20f; // Initial duration of being drunk
    private Coroutine drunkTimerCoroutine;
    public TextMeshProUGUI cashText;    


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        HandleJump();
        if (transform.position.y <= 1 || IsGrounded) {
            spriteRenderer.sprite = groundedSprite;
        }
        else {
            spriteRenderer.sprite = airborneSprite;
        }
    }

    private void HandleJump()
    {
        if (IsGrounded && _jumpPressed)
        {
            spriteRenderer.sprite = airborneSprite;

            _jumpYVel = CalculateJumpVel(jumpHeight);
            _jumpPressed = false;
            
            _moveVel = _rigidbody2D.velocity;
            _moveVel.y = _jumpYVel;
            _rigidbody2D.velocity = _moveVel;
        }

    }

    private void Move()
    {
        _moveVel = _rigidbody2D.velocity;
        if (_moveVel.x != 0) {
        _moveVel.x = _moveDir * moveSpeed *Time.fixedDeltaTime + _moveVel.x * 0.1f;
        }
        else {
            _moveVel.x = _moveDir * moveSpeed *Time.fixedDeltaTime;
        }
        _rigidbody2D.velocity = _moveVel;
    }
    

    private float CalculateJumpVel(float height)
    {
        return MathF.Sqrt((-2 * _rigidbody2D.gravityScale*Physics2D.gravity.y * height));
    }

    void GetInput()
    {
        _moveDir = Input.GetAxisRaw("Horizontal"); // takes move input
        _jumpPressed |= Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow); // takes input for jump using space
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        IsGrounded = false;
    }

public void BecomeDrunk()
    {
        if (!isDrunk)
        {
            moveSpeed += 20;
            isDrunk = true;
            if (drunkTimerCoroutine != null)
            {
                StopCoroutine(drunkTimerCoroutine);
            }
            drunkTimerCoroutine = StartCoroutine(DrunkTimer());
        }
        else
        {
            // Extend the duration and increase speed
            drunkDuration += 20f;
            moveSpeed *= 1.5f;
        }
    }

    private IEnumerator DrunkTimer()
    {
        yield return new WaitForSeconds(drunkDuration);
        isDrunk = false;
        moveSpeed = 250; // Set moveSpeed back to normal
        drunkDuration = 10f; // Reset the duration
    }

    public void Die() {
        Debug.Log("U died");
        // TODO: Implement game over logic
    }

    public void updateCashCounter() {
cashText.text = "Cash: " + Points.ToString();


    }
    
}
