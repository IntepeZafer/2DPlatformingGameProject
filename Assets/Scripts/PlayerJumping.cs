using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetBool("jump", true);
        }

        // Yere indiðinde animasyonu sýfýrla
        animator.SetBool("grounded", isGrounded());
        if (isGrounded())
        {
            animator.SetBool("jump", false);
        }
    }

    private bool isGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extraHeight, groundLayer);
        return raycastHit.collider != null;
    }
}
