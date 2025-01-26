using UnityEngine;
using UnityEngine.InputSystem;

public class DefenseController : MonoBehaviour
{
    public Animator animator;
    public GameObject shield;
    public float shieldDuration = 2f;
    public float shieldCooldown = 5f;

    private InputAction defendAction;
    private bool canDefend = true;

    public bool IsDefending { get; private set; } // Add this property

    void Awake()
    {
        var inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        Debug.Assert(inputActions != null, "Could not find the InputSystem_Actions asset");

        var playerActionMap = inputActions.FindActionMap("Player");
        Debug.Assert(playerActionMap != null, "Could not find the Player action map");

        defendAction = playerActionMap.FindAction("BearDefense");
        Debug.Assert(defendAction != null, "Could not find the BearDefense action");

        defendAction.Enable();
    }

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        Debug.Assert(animator != null, "Could not find the animator component");

        if (shield == null)
        {
            shield = this.transform.Find("Shield").gameObject;
        }
        Debug.Assert(shield != null, "Shield GameObject is not assigned in the Inspector.");
    }

    void Update()
    {
        if (canDefend && defendAction.triggered)
        {
            Defend();
        }
    }

    void Defend()
    {
        Debug.Log("Activate Shield!");
        animator.SetTrigger("Defend");
        shield.SetActive(true);
        canDefend = false;
        IsDefending = true; // Set IsDefending to true when defense starts
        Invoke(nameof(DeactivateShield), shieldDuration);
        Invoke(nameof(ResetDefense), shieldCooldown);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
        IsDefending = false; // Reset IsDefending when defense ends
    }

    void ResetDefense()
    {
        canDefend = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shield.activeSelf && collision.gameObject.CompareTag("EnemyProjectile"))
        {
            // Handle collision with enemy projectile
            Debug.Log("Shield blocked a projectile!");
            Destroy(collision.gameObject);
        }
    }
}