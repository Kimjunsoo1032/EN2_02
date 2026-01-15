using NUnit.Framework;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private GunBase gun_;
    private Cursor cursor_;

    public void Grab(GunBase gun)
    {
        if (gun_ != null)
        {
            Destroy(gun_.gameObject);
        }

        gun_ = gun;
        gun.transform.SetParent(transform);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
    }

    public bool IsGrabGun()
    {
        return gun_ != null;
    }

    public void OnTrigger()
    {
        if (!IsGrabGun()) return;
        gun_.OnTrigger();
    }

    public void OffTrigger()
    {
        if (!IsGrabGun()) return;
        gun_.OffTrigger();
    }

    void Start()
    {
        GameObject cameraObject =
            GameObject.FindGameObjectWithTag("MainCamera");
        Assert.IsNotNull(cameraObject);

        bool isFindCursor =
            cameraObject.TryGetComponent(out cursor_);
        Assert.IsTrue(isFindCursor);
    }
    private void Update()
    {
        Vector3 cursorPoint =
            cursor_.GetRaycastHit().point;

        cursorPoint.y =
            transform.position.y - cursorPoint.y;

        transform.LookAt(cursorPoint);
    }

}
