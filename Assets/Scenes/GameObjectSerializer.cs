using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectSerializer : MonoBehaviour
{

    public void SendObjectInformationToLLM(string Tag)
    {
        List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();

        // Iterate through relevant game objects
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tag))
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            objData["name"] = obj.name;
            objData["position"] = obj.transform.position.ToString();
            objData["rotation"] = obj.transform.rotation.ToString();
            // Add more properties as needed

            objects.Add(objData);
        }

        // Serialize objects to JSON or another format
        string jsonData = JsonUtility.ToJson(objects);

        // Send serialized data to the LLM via HTTP request
        StartCoroutine(SendDataToLLM(jsonData));
    }

    IEnumerator SendDataToLLM(string data)
    {
        yield return 0;
    }
}