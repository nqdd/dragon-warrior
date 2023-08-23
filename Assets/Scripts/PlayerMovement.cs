using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 10.0f;

    private Rigidbody2D body;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);
        Jump();
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
