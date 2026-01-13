using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Arm arm_;

    [SerializeField]
    private float moveSpeed_ = 10f;

    [SerializeField]
    private Cursor cursor;

    private Rigidbody rb_;
    private Vector2 moveInput_;
    private bool isPushFire_;

    private void Start()
    {
        rb_ = GetComponent<Rigidbody>();

        bool isGet = Camera.main.TryGetComponent<Cursor>(out cursor);
        Assert.IsTrue(isGet, "componentªÎö¢Ôðã÷ø¨");

        isPushFire_ = false;
    }

    private void Update()
    {
        UpdateGunTrigger();
        UpdateLook();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnFire(InputValue inputValue)
    {
        isPushFire_ = inputValue.isPressed;
    }

    public void OnMove(InputValue value)
    {
        moveInput_ = value.Get<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) { return; }
        TryGetGun(other);
    }

    private void UpdateGunTrigger()
    {
        if (!arm_.IsGrabGun()) { return; }

        if (isPushFire_)
        {
            arm_.OnTrigger();
        }
        else
        {
            arm_.OffTrigger();
        }
    }

    private void UpdateLook()
    {
        if (!cursor.GetIsHit()) { return; }

        RaycastHit raycastHit = cursor.GetRaycastHit();
        Vector3 lookAt = raycastHit.point;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);
    }

    private void Move()
    {
        Vector3 input = new Vector3(
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

    private void TryGetGun(Collider item)
    {
        GunBase gun;

        if (!item.TryGetComponent(out gun)) { return; }
        if (!gun.GetIsAlone()) { return; }

        arm_.Grab(gun);
    }
}
