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
    [SerializeField] private int nbrMovementTotal = 12;
    private int nbrMovmentLeft;

    private SpriteRenderer _sprite;

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rbHorizontal = GetComponent<Rigidbody2D>();
        rbVertical = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        nbrMovmentLeft = nbrMovementTotal;
    }


    private void Update()
    {
        
        if (nbrMovmentLeft > 0)
        {
            
            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Vertical");

           
            bool hasInput = (dirX != 0f || dirY != 0f);

            
            if (hasInput && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                nbrMovmentLeft--;
                rbHorizontal.velocity = new Vector2(dirX * moveSpeed, rbHorizontal.velocity.y);
                rbVertical.velocity = new Vector2(rbVertical.velocity.x, dirY * verticalMoveSpeed);

                
                Debug.Log("Movements Left: " + nbrMovmentLeft);
            }

            
            if (hasInput)
            {
                UpdateAnimationState();
            }
        }
        else
        {
            
            rbHorizontal.velocity = Vector2.zero;
            rbVertical.velocity = Vector2.zero;

            
            Debug.Log("No movements left.");
        }
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
}