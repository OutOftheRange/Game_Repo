using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private CapsuleCollider2D collide;
    private Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private float movementSpeed = 7f;
    private float jumpingForce = 10f;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        collide = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * movementSpeed, player.velocity.y);

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpingForce);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (dirX > 0f)
        {
            animator.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            animator.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            animator.SetBool("running", false);
        }
    }


    private bool Grounded()
    {
        return Physics2D.BoxCast(collide.bounds.center, collide.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
