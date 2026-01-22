using UnityEngine;
using System.Collections.Generic;
public class Switch : MonoBehaviour
{
    [SerializeField]
    private List<Door> targetDoors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        foreach(Door door in targetDoors)
        {
            door.AddSwitch();
        }
    }
    private void OnDestroy()
    {
        foreach(Door door in targetDoors)
        {
            if(door == null) { continue; }
            door.DestroySwitch();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
