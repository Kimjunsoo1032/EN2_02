using UnityEngine;
using UnityEngine.Assertions;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Arm myArm_;
    [SerializeField]
    float findDistance_ = 10;
    [SerializeField]
    float findAngleDeg_ = 60;
    PlayerMove player_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(playerObject);
        bool findPlayerMove=
            playerObject.TryGetComponent(out player_);
        Assert.IsTrue(findPlayerMove);
    }
    private bool IsFindPlayer()
    {
        Vector3 toPlayer =
            player_.transform.position - transform.position;
        float sqrPlayerDistance =
            Vector3.SqrMagnitude(toPlayer);
        if (sqrPlayerDistance > findDistance_ * findDistance_)
        { return false; }
        float dot = Vector3.Dot(toPlayer.normalized,
            transform.forward);
        float toPlayerAngleDeg =
            Mathf.Acos(dot) * Mathf.Rad2Deg;
        if(toPlayerAngleDeg> findAngleDeg_ / 2)
        {
            return false;
        }
        Ray ray = new Ray(transform.position, toPlayer);
        RaycastHit hit;
        int mask = ~LayerMask.GetMask("Item");
        if(!Physics.Raycast(ray, out hit,findDistance_, mask))
        {
            return false;
        }
        return hit.collider.gameObject == player_.gameObject;
    }
    private void OnTrigger()
    {
        myArm_.OnTrigger();
    }
    private void OffTrigger()
    {
        myArm_.OffTrigger();
    }
    // Update is called once per frame
    private void Update()
    {
        Debug.Log(IsFindPlayer());
        if (IsFindPlayer())
        {
            transform.LookAt(player_.transform);
            OnTrigger();
            OffTrigger();
        }
        else
        {
            OffTrigger();
        }
    }
}
