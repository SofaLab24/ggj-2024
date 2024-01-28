using UnityEngine;

public class MentasGoomba : MonoBehaviour
{
    public TriangleMarozController playerController;
    public float speed = 150f;
    private Rigidbody2D rb;
private bool facingRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = speed * Time.deltaTime;
            if (facingRight)
            {
                rb.velocity = new Vector2(move, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-move, rb.velocity.y);
            }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with obstacles to change direction
        if (other.CompareTag("Obstacle"))
        {
            Flip();
        }

        // Check for collision with player
        if (other.CompareTag("Player"))
        {
            // Get the direction of collision
            Vector2 direction = other.transform.position - transform.position;
            if (direction.y > 0)
            {
                playerController.Points += 0.02f;
                playerController.updateCashCounter();
                // Player jumps on enemy, destroy the enemy
                Destroy(gameObject);
            }
            else
            {
                playerController.Die();
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
