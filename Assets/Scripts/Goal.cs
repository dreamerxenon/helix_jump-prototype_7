using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
  public List<GameObject> GoalPrefabs = new List<GameObject>();

  void Start()
    {
        // The name of the GameObjects you want to find
        string targetName = "Ball";

        // Find all GameObjects with the specified name and add them to the list
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(targetName); 

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == targetName)
            {
                GoalPrefabs.Add(obj);
            }
        }
    }


}
