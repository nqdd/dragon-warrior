using UnityEngine;

enum PlayerState {
    IDLE,
    RUN,
    JUMP
}

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 10.0f;

    private Rigidbody2D body;
    private Animator animator;


    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");

        ChangePlayerState(horizontalInput);

        Move(horizontalInput);

        Jump();
    }

    private void ChangePlayerState(float horizontalInput) {
        bool isOnGround = body.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isOnGround && horizontalInput != 0) {
            this.SetAnimation(PlayerState.RUN);
            return;
        }

        if (!isOnGround) {
            this.SetAnimation(PlayerState.JUMP);
            return;
        }

        this.SetAnimation(PlayerState.IDLE);
    }

    private void SetAnimation(PlayerState state) {
        animator.SetInteger("state", (int)state);
    }

    private void Move(float horizontalInput) {
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput == 0) {
            return;
        }

        int xCoodirnate = horizontalInput > 0 ? 1 : -1;
        transform.localScale = new Vector3(xCoodirnate, 1, 1);
    }

    void Jump() {
        bool isOnGround = body.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isOnGround && Input.GetKey(KeyCode.Space)) {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }


}
