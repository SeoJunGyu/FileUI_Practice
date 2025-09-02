using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private PlayerInput input;

    private Rigidbody rigid;
    private Vector3 moveDir;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveDir = new Vector3(input.Hor, 0f, input.Ver);
        rigid.MovePosition(transform.position + (moveDir * moveSpeed * Time.fixedDeltaTime));
    }
}
