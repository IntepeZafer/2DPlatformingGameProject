using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body; // The Rigidbody2D component
    private Animator animator; // The Animator component
    [SerializeField] private float speed; // The speed of the player
    private bool grounded; // If the player is on the ground
    private void Awake() // Awake is called before Start 
    {
        body = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component 
        animator = GetComponent<Animator>(); // Get the Animator component 
    }
    private void Update() // Update is called once per frame 
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal input 
        body.velocity = new Vector2(horizontalInput * speed ,body.velocity.y); // Move the player horizontally 
        if (horizontalInput > 0.01f) // If the player is moving to the right 
        {
            transform.localScale = Vector3.one; // If the player is moving to the right, flip the sprite 
        }
        else if (horizontalInput < -0.01f) // If the player is moving to the left 
        {
            transform.localScale = new Vector3(-1, 1, 1); // If the player is moving to the left, flip the sprite
        }
        if (Input.GetKey(KeyCode.Space) && grounded) // If the player presses space and is on the ground 
        { 
            Jump(); // Jump the player
        }
        animator.SetBool("run" , horizontalInput != 0); // Set the run animation to true if the player is moving
        animator.SetBool("grounded", grounded); // Set the grounded animation to true if the player is on the ground 
    }
    private void Jump() // Jump the player 
    {
        body.velocity = new Vector2(body.velocity.x, speed); // Jump the player up  
        animator.SetBool("jump", true); // Set the jump animation to true
        grounded = false; // Set the grounded variable to false 
    }
    private void OnCollisionEnter2D(Collision2D collision) // If the player collides with something 
    {
        if(collision.gameObject.tag == "Ground") // If the player collides with the ground 
        {
            grounded = true; // Set the grounded variable to true 
        }
    }
}
