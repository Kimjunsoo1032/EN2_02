using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayBullet : MonoBehaviour
{
    [SerializeField] private float lifeTime_ = 0.5f;

    private float timer_;
    private LineRenderer line_;

    private Vector3 beginPosition_;
    private Vector3 endPosition_;

    private void Awake()
    {
        line_ = GetComponent<LineRenderer>();

        line_.positionCount = 2;

        timer_ = lifeTime_;
    }

    public void SetPositions(Vector3 beginPosition, Vector3 endPosition)
    {
        beginPosition_ = beginPosition;
        endPosition_ = endPosition;

        line_.SetPosition(0, beginPosition_);
        endPosition_=new Vector3(endPosition_.x, endPosition_.y+1.5f, endPosition_.z);
        line_.SetPosition(1, endPosition_);
        Debug.Log(endPosition_);
    }

    private void Update()
    {
        timer_ -= Time.deltaTime;
        if (timer_ <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
