using UnityEngine;

enum PlayerState {
    IDLE,
    WALK,
    JUMP
}

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 10.0f;

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Jump();
    }

    private void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if (horizontalInput != 0) {
            animator.SetInteger("state", (int)PlayerState.WALK);
            if (horizontalInput > 0.01f) {
                transform.localScale = Vector3.one;
            } else if (horizontalInput < -0.01f) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        } else {
            animator.SetInteger("state", (int)PlayerState.IDLE);
        }
    }

    private void Jump() {
        var isGround = body.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGround) {
            if (Input.GetKey(KeyCode.Space)) {
                body.velocity = new Vector2(body.velocity.x, speed);
            }
        } else {
            animator.SetInteger("state", (int)PlayerState.JUMP);
        }
    }
}
