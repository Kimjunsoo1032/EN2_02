using UnityEngine;

public class Door : MonoBehaviour
{
    private int switchNum = 0;
    private int destroyedSwitchNum = 0;
    public void AddSwitch()
    {
        switchNum++;
    }
    public void DestroySwitch()
    {
        destroyedSwitchNum++;
        if (switchNum > destroyedSwitchNum) { return; }
        Destroy(gameObject);
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
