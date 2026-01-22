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
        CheckGrabGun();
    }
    private void Update()
    {
        if (!OwnerIsPlayer()) { return; }
        Vector3 cursorPoint =
            cursor_.GetRaycastHit().point;

        cursorPoint.y =
            transform.position.y - cursorPoint.y;

        transform.LookAt(cursorPoint);
    }
    private void CheckGrabGun()
    {
        Assert.IsTrue(
            transform.childCount <= 1,
            "一つの腕に複数の銃が割り当てられています。"
    );
        if (transform.childCount == 0) { return; }
        GunBase grabGun;
        bool hasGun =
            transform.GetChild(0).TryGetComponent(out grabGun);
        if (hasGun) { Grab(grabGun); }
    }
    private bool OwnerIsPlayer()
    {
        if (transform.parent == null) { return false; }
        return transform.parent.tag == "Player";
    }

}
