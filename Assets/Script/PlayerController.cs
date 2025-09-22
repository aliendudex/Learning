using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
        // Example: flash red to simulate attack
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetColor), 0.2f);
    }

    private void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        // Screen wrap
        // Screen wrap (dynamic, based on camera size)
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Camera.main.aspect;

        Vector3 pos = transform.position;
        if (pos.x > horzExtent) pos.x = -horzExtent;
        else if (pos.x < -horzExtent) pos.x = horzExtent;
        if (pos.y > vertExtent) pos.y = -vertExtent;
        else if (pos.y < -vertExtent) pos.y = vertExtent;
        transform.position = pos;

        /*Vector3 pos = transform.position;
        if (pos.x > 9) pos.x = -9;
        else if (pos.x < -9) pos.x = 9;
        if (pos.y > 5) pos.y = -5;
        else if (pos.y < -5) pos.y = 5;
        transform.position = pos;*/
    }
}
