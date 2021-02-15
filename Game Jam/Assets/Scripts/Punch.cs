using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public bool isPunching = false;
    public int punchDamage;

    private void OnCollisionEnter2D(Collision2D other) {
        if (isPunching) {
            if (other.gameObject.tag.Contains("Enemy")) {
                EnemyScript enermyscript = other.gameObject.GetComponent(typeof(EnemyScript)) as EnemyScript;

                if (transform.position.x - other.transform.position.x < 0) {
                    other.rigidbody.velocity = new Vector2(18, 0);
                } else {
                    other.rigidbody.velocity = new Vector2(-18, 0);
                }

                if (enermyscript != null) {
                    enermyscript.health -= punchDamage;
                }
            }
        }
    }
}
