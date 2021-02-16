using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireFlyScript : MonoBehaviour
{
    public int speed;
    public float health;
    public float damage;
    private Rigidbody2D rb;
    public MonoBehaviour[] allEnemys;
    private Transform player;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        allEnemys = GameObject.FindObjectsOfType<MonoBehaviour>();
        player = GameObject.Find("player").GetComponent<Transform>();
    }

    private float distance(GameObject a, GameObject b) {
        return Mathf.Sqrt(Mathf.Pow(a.transform.position.x - b.transform.position.x, 2) + Mathf.Pow(a.transform.position.y - b.transform.position.y, 2) + Mathf.Pow(a.transform.position.z - b.transform.position.z, 2));
    }

    private void Update() {
        float bestScore = Mathf.Infinity;
        int bestIndex = -1;
        for (var i = 0; i < allEnemys.Length; i++) {
            if (allEnemys[i] != null) {
                if (allEnemys[i].gameObject.tag.Contains("Enemy1")) {
                    if (distance(gameObject, allEnemys[i].gameObject) < bestScore) {
                        bestScore = distance(gameObject, allEnemys[i].gameObject);
                        bestIndex = i;
                    }
                }
            }
        }
        if (bestIndex != -1) {
            transform.rotation = Quaternion.FromToRotation(transform.up, allEnemys[bestIndex].gameObject.transform.position - transform.position) * transform.rotation;
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.rotation = new Quaternion(0, 0, transform.rotation.z, 0);
        } else {
            transform.rotation = Quaternion.FromToRotation(transform.up, player.position - transform.position) * transform.rotation;
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.rotation = new Quaternion(0, 0, transform.rotation.z, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Contains("Enemy")) {
            other.gameObject.GetComponent<EnemyScript>().health -= damage;
            health -= damage;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
