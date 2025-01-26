using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 1f;
    public float attackRange = 1f;
    public int attackDamage = 10;
    public Vector2 attackBoxSize = new Vector2(1f, 1f); // Size of the attack box
    public LineRenderer lineRenderer;
    public string attackTrigger = "Attack";
    public string targetTag = "Enemy"; // Tag to identify targets

    private bool canAttack = true;
    private Vector2 currentMoveDirection;
    private Vector2 attackDirectionOverride;

    public bool IsAttacking { get; private set; } // Add this property

    public void SetMoveDirection(Vector2 moveDirection)
    {
        currentMoveDirection = moveDirection;
    }

    public void SetAttackDirection(Vector2 direction)
    {
        attackDirectionOverride = direction;
    }

    public void TriggerAttack()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Trigger Attack!");
        animator.SetTrigger(attackTrigger);
        canAttack = false;
        IsAttacking = true; // Set IsAttacking to true when attack starts
        Invoke(nameof(ResetAttack), attackCooldown);

        // Draw the attack direction
        Vector2 attackDirection = GetAttackDirection();
        DrawAttackDirection(attackDirection);

        // Detect and apply damage to targets in the attack range
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRange;
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPosition, attackBoxSize, 0f);
        foreach (var hit in hits)
        {
            if (hit != null && hit.CompareTag(targetTag) && hit.gameObject != gameObject)
            {
                Health targetHealth = hit.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
        IsAttacking = false; // Reset IsAttacking when attack ends
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false; // Hide the line after the attack
        }
    }

    private Vector2 GetAttackDirection()
    {
        // Use the attack direction override if set, otherwise use the current move direction
        if (attackDirectionOverride != Vector2.zero)
        {
            return attackDirectionOverride.normalized;
        }
        return currentMoveDirection.normalized;
    }

    private void DrawAttackDirection(Vector2 direction)
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (Vector3)direction * attackRange);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the attack box in the editor for visualization
        Gizmos.color = Color.red;
        Vector2 attackPosition = (Vector2)transform.position + GetAttackDirection() * attackRange;
        Gizmos.DrawWireCube(attackPosition, attackBoxSize);
    }
}