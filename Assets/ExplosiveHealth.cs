using UnityEngine;

public class ExplosiveHealth : Health
{
    [SerializeField]
    Explosion explosionPrefab_;
    protected override void Death()
    {
        Instantiate(explosionPrefab_, transform.position,
            Quaternion.identity);

        base.Death();
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
