using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed;
    public int fireBallDamage;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Contains("Enemy")) {
            other.gameObject.GetComponent<EnemyScript>().health -= fireBallDamage;
        }
        Destroy(gameObject);
    }
}
