using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipAttack : MonoBehaviour
{
    public Collider2D whipCollider;
    public float damage = 3;
    private Vector2 initialPosition;

    private void Start() {
        initialPosition = transform.localPosition;
    }

    public void AttackRight() {
        whipCollider.enabled = true;
        transform.localPosition = initialPosition;
    }

    public void AttackLeft() {
        whipCollider.enabled = true;
        transform.localPosition = new Vector3(initialPosition.x * -1, initialPosition.y);
    }

    public void StopAttack() {
        whipCollider.enabled = false;
        transform.localPosition = initialPosition; // Reset to initial position after attack
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null) {
                enemy.Health -= damage;
            }
        }
    }
}
