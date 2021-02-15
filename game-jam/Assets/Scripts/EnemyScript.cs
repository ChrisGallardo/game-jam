using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform playerTransform;
    public int maxDistanceToAgress;
    public int speed;
    private Rigidbody2D rb;
    public int damage;
    public int health;
    private PlayerMovement player;
    
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("player").GetComponent<Transform>();
        player = GameObject.Find("player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate() {
        float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - playerTransform.position.x, 2) + Mathf.Pow(transform.position.y - playerTransform.position.y, 2) + Mathf.Pow(transform.position.z - playerTransform.position.z, 2));
        if (distance <= maxDistanceToAgress) {
            if (transform.position.x - playerTransform.position.x < 0) {
                rb.velocity = new Vector2(rb.velocity.x + speed, 0);
                if (rb.velocity.x > 1) {
                    rb.velocity = new Vector2(rb.velocity.x - 1, 0);
                }
            } else {
                rb.velocity = new Vector2(rb.velocity.x + -speed, 0);
                if (rb.velocity.x < -1) {
                    rb.velocity = new Vector2(rb.velocity.x + 1, 0);
                }
            }
        } else {
            rb.velocity = new Vector2(0, 0);
        }
    }   

    private void Update() {
        if (health <= 0) {
            player.souls += 1;
            Destroy(gameObject);
        }
    }
}