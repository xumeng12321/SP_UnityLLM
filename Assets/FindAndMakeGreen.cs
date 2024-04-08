using System.Collections.Generic;
using UnityEngine;

public class FindAndMakeGreen : MonoBehaviour
{
    void Start()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        Debug.Log("Found " + fruits.Length + " fruits");

        foreach (GameObject fruit in fruits)
        {
            Renderer renderer = fruit.GetComponent<Renderer>();
            Debug.Log("Found renderer: " + renderer);
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }
        }
    }
}