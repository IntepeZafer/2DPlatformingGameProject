using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private void Awake() // Awake is called before Start
    {
        body = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }
    private void Update() // Update is called once per frame
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed ,body.velocity.y); // Move the player
        if (Input.GetKey(KeyCode.Space)) // If the player presses space
        { 
            body.velocity = new Vector2(body.velocity.x, speed); // Jump
        } 
    }
}
