using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 1f;
    public LineRenderer lineRenderer;
    public float attackRange = 1f;

    private InputAction attackAction;
    private bool canAttack = true;
    private Vector2 currentMoveDirection;

    public bool IsAttacking { get; private set; } // Add this property

    void Awake()
    {
        var inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        Debug.Assert(inputActions != null, "Could not find the InputSystem_Actions asset");

        var playerActionMap = inputActions.FindActionMap("Player");
        Debug.Assert(playerActionMap != null, "Could not find the Player action map");

        attackAction = playerActionMap.FindAction("BoyAttack");
        Debug.Assert(attackAction != null, "Could not find the BoyAttack action");

        attackAction.Enable();
    }

    void Update()
    {
        if (canAttack && attackAction.triggered)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Trigger Attack!");
        animator.SetTrigger("BoyAttack");
        canAttack = false;
        IsAttacking = true; // Set IsAttacking to true when attack starts
        Invoke(nameof(ResetAttack), attackCooldown);

        // Draw the attack direction
        Vector2 attackDirection = GetAttackDirection();
        DrawAttackDirection(attackDirection);
    }

    void ResetAttack()
    {
        canAttack = true;
        IsAttacking = false; // Reset IsAttacking when attack ends
        lineRenderer.enabled = false; // Hide the line after the attack
    }

    Vector2 GetAttackDirection()
    {
        // Use the current move direction as the attack direction
        return currentMoveDirection.normalized;
    }

    void DrawAttackDirection(Vector2 direction)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + (Vector3)direction * attackRange);
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        currentMoveDirection = moveDirection;
    }
}