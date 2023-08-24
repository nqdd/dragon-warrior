using UnityEngine;
using Unity.VisualScripting;

enum PlayerState : int {
    IDLE = 0,
    RUN = 1,
    JUMP = 2,
    ATTACK = 3
}

public class PlayerAnimation : MonoBehaviour {

    private Rigidbody2D body;
    private Animator animator;

    private PlayerAttack playerAttack;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        ChangePlayerState(horizontalInput);
    }

    private void ChangePlayerState(float horizontalInput) {

        if (Input.GetMouseButton((int)MouseButton.Left)) {
            this.SetAnimation(PlayerState.ATTACK);
            return;
        }

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
}
