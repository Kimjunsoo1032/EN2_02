using UnityEngine;

public class Arm : MonoBehaviour
{
    private GunBase gun_;
    public void Grab(GunBase gun)
    {
        if (gun_ != null) { Destroy(gun_.gameObject); }
        gun_ = gun;
        gun.transform.SetParent(transform);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation =
            Quaternion.identity;
    }
    public bool IsGrabGun()
    {
        return gun_ != null;
    }
    public void OnTrigger()
    {
        if (!IsGrabGun()) { return; }
        gun_.OnTrigger();
    }
    public void OffTrigger()
    {
        if (!IsGrabGun()) { return; }
        gun_.OffTrigger();
    }
// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{

}

// Update is called once per frame
void Update()
{

}
}
