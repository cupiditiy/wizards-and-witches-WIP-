using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAttack : MonoBehaviour
{
    public enum AttackDirection { 
        left, right
    }
    public AttackDirection attackDirection;

    Vector2 rightAttackOffset;
    Collider2D orbCollider;
    private void Start() {
        orbCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }

    public void Attack() {
        switch (attackDirection) { 
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
        }
    }
 public void AttackRight() {
        orbCollider.enabled = true;
        transform.position = rightAttackOffset;
    }
    public void AttackLeft() {
        orbCollider.enabled = true;
        transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }
    public void StopAttack() { 
        orbCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") { }
    }
}