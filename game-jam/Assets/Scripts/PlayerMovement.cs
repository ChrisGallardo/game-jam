using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public int speed;
    public double playerLight;
    public int maxPlayerLight;
    public int fireflys;
    public int souls;
    private Rigidbody2D rb;
    public LayerMask enemyLayer;
    private bool animHasBeenActivated = false;
    private Punch armR;
    public GameObject fireball;
    private GameObject newInstance;
    public int fireBallCost;

    private void Awake() {
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        armR = GameObject.Find("ArmR").GetComponent<Punch>();
    }

    private void FixedUpdate() {
        float direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            animHasBeenActivated = false;
            armR.isPunching = false;
        }
        if (direction != 0) {
            anim.Play("Player Walk");
            animHasBeenActivated = false;
        } else {
            if (!animHasBeenActivated) {
                anim.Play("New State");
                animHasBeenActivated = true;
            }
        }
    }

    private void Update() {
        if (playerLight > maxPlayerLight) {
            playerLight = maxPlayerLight;
        }
        if (Input.GetMouseButtonDown(0)) {
            animHasBeenActivated = true;
            anim.Play("Punch");
            armR.isPunching = true;
        }
        if (Input.GetMouseButtonDown(1)) {
            playerLight -= fireBallCost;
            newInstance = Instantiate(fireball, new Vector3(transform.position.x + 2, transform.position.y + 2.5f, transform.position.z), new Quaternion(0, 0, 0, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Contains("Enemy")) {
            EnemyScript enermyscript = other.gameObject.GetComponent(typeof(EnemyScript)) as EnemyScript;

            if (enermyscript != null && !armR.isPunching) {
                playerLight -= enermyscript.damage;
                if (transform.position.x - other.transform.position.x < 0) {
                    other.rigidbody.velocity = new Vector2(18, 0);
                } else {
                    other.rigidbody.velocity = new Vector2(-18, 0);
                }
            }
        }
    }
}