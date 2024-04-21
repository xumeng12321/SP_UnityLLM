using UnityEngine;
using System.Collections;
using Callables;

public class NewCode : MonoBehaviour
{
    public void Start() 
    {
        foreach (GameObject gameobject in GameObject.FindGameObjectsWithTag("AI")) 
        {
            CallableMethods.PerformJump(gameobject, 0.7f);
        } 
    } 
}