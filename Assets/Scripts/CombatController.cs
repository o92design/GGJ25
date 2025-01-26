using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    public AttackController attackController;

    private InputAction attackAction;

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
        if (attackAction.triggered)
        {
            attackController.TriggerAttack();
        }
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        attackController.SetMoveDirection(moveDirection);
    }
}