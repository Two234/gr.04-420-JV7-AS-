using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbHorizontal; 
    private Rigidbody2D rbVertical;   
    private BoxCollider2D coll;

    private float dirX;
    private float dirY; 
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float verticalMoveSpeed = 5f; 
    [SerializeField] private LayerMask groundLayer;
    private SpriteRenderer _sprite;

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rbHorizontal = GetComponent<Rigidbody2D>();
        rbVertical =GetComponent<Rigidbody2D>(); 
        _sprite = GetComponent<SpriteRenderer>();
    }

   
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

       
        rbHorizontal.velocity = new Vector2(dirX * moveSpeed, rbHorizontal.velocity.y);

        
        rbVertical.velocity = new Vector2(rbVertical.velocity.x, dirY * verticalMoveSpeed);

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            _sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            _sprite.flipX = true;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
}