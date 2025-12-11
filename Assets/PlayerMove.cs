using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed_ = 10f;
    private Rigidbody rb_;
    private Vector2 moveInput_;
    [SerializeField]
    private Cursor cursor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        bool isGet =
            Camera.main.TryGetComponent<Cursor>(out cursor);
        Assert.IsTrue(isGet, "componentªÎö¢Ôðã÷ø¨");
    }

    // Update is called once per frame
    private void Update()
    {
        if (!cursor.GetIsHit()){ return; }
        RaycastHit raycasthit = cursor.GetRaycastHit();
        Vector3 lookAt = raycasthit.point;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }
    public void OnMove(InputValue value)
    {
        moveInput_ = value.Get<Vector2>();
    }
    void FixedUpdate()
    {
        Vector3 input;
        input = new Vector3(
            moveInput_.x,
            0,
            moveInput_.y
        );
        if (input.sqrMagnitude == 0) { return; }

        rb_.MovePosition(
            transform.position +
            input * moveSpeed_ * Time.deltaTime
        );
    }
}
