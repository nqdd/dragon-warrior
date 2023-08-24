using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] private float attackCooldown = 0.25f;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile[] fireBalls;

    private PlayerMovement playerMovement;

    public bool isCoolDown { get { return cooldownTimer < attackCooldown; } }

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update() {
        Attack();
    }

    private void Attack() {
        if (Input.GetMouseButton((int)MouseButton.Left) && cooldownTimer > attackCooldown) {
            var index = GetIndexFireBallActive();
            fireBalls[index].transform.position = firePoint.transform.position;
            fireBalls[index].SetDirection(Mathf.Sign(playerMovement.transform.localScale.x));
            fireBalls[index].Fire();
            cooldownTimer = 0;
        }

        cooldownTimer += Time.deltaTime;
    }

    private int GetIndexFireBallActive() {
        for (int index = 0; index < fireBalls.Length; index++) {
            if (!fireBalls[index].isActivate) return index;
        }
        return 0;
    }

}
