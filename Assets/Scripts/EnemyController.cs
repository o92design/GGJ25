using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;
    public int damage = 10;
    public float attackCooldown = 2f; // Cooldown duration in seconds

    public Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private bool canAttack = true;

    public enum State { Idle, Chase, Attack }
    public State currentState = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdleState();
                break;
            case State.Chase:
                HandleChaseState();
                break;
            case State.Attack:
                HandleAttackState();
                break;
        }
    }

    void HandleIdleState()
    {
        target = GetClosestTarget();
        if (target != null && Vector2.Distance(transform.position, target.position) <= chaseRange)
        {
            currentState = State.Chase;
        }
    }

    void HandleChaseState()
    {
        target = GetClosestTarget();
        if (target == null || Vector2.Distance(transform.position, target.position) > chaseRange)
        {
            currentState = State.Idle;
        }
        else if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            currentState = State.Attack;
        }
        else if (canAttack) // Ensure the enemy only moves if it is not attacking
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    void HandleAttackState()
    {
        if (target == null || Vector2.Distance(transform.position, target.position) > attackRange)
        {
            currentState = State.Chase;
        }
        else if (canAttack)
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    Transform GetClosestTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = player.transform;
            }
        }

        return closestTarget;
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    // This method can be called by the attack animation event
    public void DealDamage()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            // Assuming the target has a method to take damage
            //target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}