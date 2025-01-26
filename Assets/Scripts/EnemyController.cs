using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AttackController attackController;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;

    private Transform target;
    private Rigidbody2D rb;
    private Health health;

    private enum State { Idle, Chase, Attack }
    private State currentState = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (health.currentHealth <= 0)
        {
            return; // Stop updating if the enemy is dead
        }

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
        else
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
        else
        {
            Vector2 attackDirection = (target.position - transform.position).normalized;
            attackController.SetAttackDirection(attackDirection);
            attackController.TriggerAttack();
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
}