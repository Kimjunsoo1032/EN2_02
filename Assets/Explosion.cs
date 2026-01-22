using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    float damage_ = 5;
    [SerializeField]
    float extintionTime_ = 1;
    float timer_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer_ = extintionTime_;
    }

    // Update is called once per frame
    void Update()
    {
        timer_ -= Time.deltaTime;
        if (timer_ > 0) { return; }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Health health;
        bool hasHealth =
            other.TryGetComponent(out health);
        if (!hasHealth) { return; }
        health.Damage(damage_);
    }
}
