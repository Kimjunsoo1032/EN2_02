using UnityEngine;

public class HandGun : GunBase
{
    [SerializeField] private Camera aimCamera_;
    [SerializeField] private float rayLength_ = 200f;

    public override void OffTrigger() { }

    public override void OnTrigger()
    {
        if (shotTimer_ > 0f) return;
        shotTimer_ = fireRate_;

        if (aimCamera_ == null)
        {
            aimCamera_ = Camera.main;
            if (aimCamera_ == null) return;
        }

        int layerMask = ~LayerMask.GetMask("Item");

        Ray camRay = aimCamera_.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPoint = camRay.origin + camRay.direction * rayLength_;

        if (Physics.Raycast(camRay, out RaycastHit hit, rayLength_, layerMask))
        {
            targetPoint = hit.point;

            if (hit.collider.TryGetComponent(out Health health))
            {
                health.Damage(power_);
            }
        }

        Vector3 muzzlePos = muzzleTransform_.position;

        GameObject bulletObj = Instantiate(bulletPrefab_.gameObject, muzzlePos, Quaternion.identity);
        bulletObj.GetComponent<RayBullet>().SetPositions(muzzlePos, targetPoint);
    }

    public override void Update()
    {
        base.Update();
    }
}
