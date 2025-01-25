using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float springDistance = 1f;
    public float springDampingRatio = 0.5f;
    public float springFrequency = 1f;

    private Transform boyTransform;
    private Transform bearTransform;

    private Rigidbody2D boyRb;
    private Rigidbody2D bearRb;
    private Vector2 boyMovement;
    private Vector2 bearMovement;

    private InputAction moveBoyAction;
    private InputAction moveBearAction;

    void Awake()
    {
        var inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        Debug.Assert(inputActions != null, "Could not find the InputSystem_Actions asset");

        var playerActionMap = inputActions.FindActionMap("Player");
        Debug.Assert(playerActionMap != null, "Could not find the Player action map");

        moveBoyAction = playerActionMap.FindAction("MoveBoy");
        moveBearAction = playerActionMap.FindAction("MoveBear");

        Debug.Assert(moveBoyAction != null, "Could not find the MoveBoy action");
        Debug.Assert(moveBearAction != null, "Could not find the MoveBear action");

        moveBoyAction.Enable();
        moveBearAction.Enable();
    }

    void Start()
    {
        boyTransform = transform.Find("Boy");
        bearTransform = transform.Find("Bear");

        Debug.Assert(boyTransform != null, "Could not find the Boy transform");
        Debug.Assert(bearTransform != null, "Could not find the Bear transform");

        boyRb = boyTransform.GetComponent<Rigidbody2D>();
        bearRb = bearTransform.GetComponent<Rigidbody2D>();

        Debug.Assert(boyRb != null, "Could not find the Boy Rigidbody2D");
        Debug.Assert(bearRb != null, "Could not find the Bear Rigidbody2D");

        // Configure the SpringJoint2D on the Bear
        var springJoint = bearTransform.GetComponent<SpringJoint2D>();
        if (springJoint == null)
        {
            springJoint = bearTransform.gameObject.AddComponent<SpringJoint2D>();
        }
        springJoint.connectedBody = boyRb;
        springJoint.autoConfigureDistance = false;
    }

    void Update()
    {
        // Get input from the right stick for the boy
        boyMovement = moveBoyAction.ReadValue<Vector2>();

        // Get input from the left stick for the bear
        bearMovement = moveBearAction.ReadValue<Vector2>();

        // Update SpringJoint2D properties in real-time
        var springJoint = bearTransform.GetComponent<SpringJoint2D>();
        springJoint.distance = springDistance;
        springJoint.dampingRatio = springDampingRatio;
        springJoint.frequency = springFrequency;
    }

    void FixedUpdate()
    {
        // Move the boy
        boyRb.MovePosition(boyRb.position + boyMovement * moveSpeed * Time.fixedDeltaTime);

        // Move the bear
        bearRb.MovePosition(bearRb.position + bearMovement * moveSpeed * Time.fixedDeltaTime);
    }
}