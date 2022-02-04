using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator Animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;


    private float dirX;

    [SerializeField] private int MovementSpeed;
    [SerializeField] private int JumpSpeed;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource JumpSoundEffect;


    private enum MovementState { idle, running, jumping, falling };

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateAnimation();
    }

    void Move()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(dirX * MovementSpeed, rigidbody2D.velocity.y);
       
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            JumpSoundEffect.Play();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpSpeed);
        }
    }

    void UpdateAnimation()
    {
        MovementState State;

        if (dirX > 0)
        {
            spriteRenderer.flipX = false;
            State = MovementState.running;
        }
        else if (dirX < 0)
        {
            spriteRenderer.flipX = true;
            State = MovementState.running;
        }
        else
        {
            State = MovementState.idle;
        }

        if (rigidbody2D.velocity.y > 0.1)
        {
            State = MovementState.jumping;
        }
        else if (rigidbody2D.velocity.y < -0.1)
        {
            State = MovementState.falling;
        }

        Animator.SetInteger("State", (int)State);
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
