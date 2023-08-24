using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 1.0f;
    private Animator animator;
    private float direction = 1;

    public bool isActivate { get { return gameObject.activeInHierarchy; } }

    private void Awake() {
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(direction * movementSpeed, 0, 0);
    }

    public void SetDirection(float value) {
        direction = value;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != value ) {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }

    public void Fire() {
        gameObject.SetActive(true);
    }

    public void Deactive() {
        gameObject.SetActive(false);
    }
}
