using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body; // The Rigidbody2D component
    private Animator animator; // The Animator component
    [SerializeField] private float speed; // The speed of the player
    [SerializeField] private float jumpPower; // The jump power of the player
    //private bool grounded; // If the player is on the ground
    private BoxCollider2D boxCollider; // The BoxCollider2D component 
    [SerializeField] private LayerMask groundLayer; // The layer mask
    [SerializeField] private LayerMask wallLayer; // The layer mask 
    private float wallJumpCooldown; // The wall jump cooldown
    private float horizontalInput; // The horizontal input 
    private void Awake() // Awake is called before Start 
    {
        body = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component 
        animator = GetComponent<Animator>(); // Get the Animator component 
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
    }
    private void Update() // Update is called once per frame 
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal input 
        body.velocity = new Vector2(horizontalInput * speed ,body.velocity.y); // Move the player horizontally 
        if (horizontalInput > 0.01f) // If the player is moving to the right 
        {
            transform.localScale = Vector3.one; // If the player is moving to the right, flip the sprite 
        }
        else if (horizontalInput < -0.01f) // If the player is moving to the left 
        {
            transform.localScale = new Vector3(-1, 1, 1); // If the player is moving to the left, flip the sprite
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded()) // If the player is pressing the space key and the player is on the ground
        { 
            Jump(); // Jump the player
        }
        animator.SetBool("run" , horizontalInput != 0); // Set the run animation to true if the player is moving
        animator.SetBool("grounded", isGrounded());  // Set the grounded animation to true if the player is on the ground
        if(wallJumpCooldown < 0.2f) // If the wall jump cooldown is less than 0.2f 
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); // Move the player horizontally 
            if(onWall() && !isGrounded()) // If the player is on the wall and the player is not on the ground 
            {
                body.gravityScale = 0; // Set the gravity scale to 0
                body.velocity = Vector2.zero; // Set the velocity to 0 
            }
            else
            {
                body.gravityScale = 7; // Set the gravity scale to 1
            }
            if (Input.GetKey(KeyCode.Space)) // If the player is pressing the space key 
            {
                Jump(); // Jump the player
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime; // Add time to the wall jump cooldown 
        }
    }
    private void Jump() // Jump the player 
    {
        if (isGrounded()) // If the player is on the ground 
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); // Jump the player up  
            animator.SetBool("jump", true); // Set the jump animation to true
        }
        else if(onWall() && !isGrounded()) // If the player is on the wall and the player is not on the ground 
        {
            if(horizontalInput == 0) // If the player is not moving horizontally 
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0); // Jump the player up 
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Flip the sprite
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6); // Jump the player up
            }
            wallJumpCooldown = 0; // Set the wall jump cooldown to 0
             
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // If the player collides with something 
    {
        
    }
    private bool isGrounded() // If the player is on the ground 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center , boxCollider.bounds.size , 0 , Vector2.down , 0.1f , groundLayer); // Cast a ray down from the player to check if the player is on the ground 
        return raycastHit.collider != null; // If the ray hits something return true 
    }
    private bool onWall() // If the player is on the ground 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f,wallLayer); // Cast a ray down from the player to check if the player is on the ground
        return raycastHit.collider != null; // If the ray hits something return true 
    }
}
