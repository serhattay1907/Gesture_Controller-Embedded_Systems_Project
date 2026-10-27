using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public SerialController serialController;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    private CharacterController cc;
    private float fixedY; // Stores the starting height and never changes

    void Start()
    {
        cc = GetComponent<CharacterController>();
        fixedY = transform.position.y; // Remember starting height

        if (serialController == null)
        {
            Debug.LogError("SerialController is not assigned!");
        }
    }

    void Update()
    {
        if (serialController == null) return;

        string command = serialController.LastCommand;

        Vector3 moveDirection = Vector3.zero;

        if (command == "LEFT")
        {
            moveDirection = Vector3.left;
        }
        else if (command == "RIGHT")
        {
            moveDirection = Vector3.right;
        }
        else if (command == "FORWARD")
        {
            moveDirection = Vector3.forward;
        }
        else if (command == "BACK")
        {
            moveDirection = Vector3.back;
        }

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        // Move the character
        cc.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Lock the height — never go up or down
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}