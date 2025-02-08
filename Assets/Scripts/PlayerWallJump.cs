using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerMovement movement;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallJumpPowerX = 5f;
    [SerializeField] private float wallJumpPowerY = 10f;

    private float wallJumpCooldown;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (onWall() && !isGrounded())
        {
            body.velocity = new Vector2(0, body.velocity.y); // Duvara yapýþma efekti
        }

        if (Input.GetKeyDown(KeyCode.Space) && onWall())
        {
            WallJump();
        }

        wallJumpCooldown += Time.deltaTime;
    }

    private void WallJump()
    {
        if (movement.GetHorizontalInput() == 0)
        {
            // Duvara yapýþmýþ halde yukarý doðru atlama
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpPowerX, wallJumpPowerY);
        }
        else
        {
            // Duvara çarpýp geri sekme
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * (wallJumpPowerX / 2), wallJumpPowerY);
        }
        wallJumpCooldown = 0;
    }

    private bool onWall()
    {
        float extraWidth = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, new Vector2(transform.localScale.x, 0), boxCollider.bounds.extents.x + extraWidth, wallLayer);
        return raycastHit.collider != null;
    }

    private bool isGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extraHeight, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }
}
