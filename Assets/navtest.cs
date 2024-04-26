using System.Collections;
using UnityEngine;
using Callables;

public class navtest : MonoBehaviour
{
    void Start()
    {
        
        CallableMethods.PickUpObject(GameObject.Find("Strawberry"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CallableMethods.PickUpObject(GameObject.Find("Strawberry"));
    }
}
