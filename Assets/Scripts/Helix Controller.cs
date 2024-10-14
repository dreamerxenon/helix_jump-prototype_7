using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 startRotation;
    private Vector2 lastTapPos;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;
    public List<Stages> allStages = new List<Stages>();
    private float helixDistance;
    public List<GameObject> spawnedLevels = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
            {
                lastTapPos = currTapPos;
            }
            float delta = lastTapPos.x - currTapPos.x;
            lastTapPos = currTapPos;

            transform.Rotate(Vector3.up * delta);
        }
        if(Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }
    public void LoadStage(int stageNumber)
    {
      Stages stage = allStages[Mathf.Clamp(stageNumber, 0 , allStages.Count - 1)];

      if (stage == null)
      {
        Debug.LogError("No stages" + stageNumber + "found in allStages list, are all stages assigned in the list");
        return;
      }
             //change background color of stage
            Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
            //change ball color on stage
            foreach (GameObject obj in  GameObject.FindGameObjectsWithTag("Ball"))
        {
            // Access the Renderer component of the GameObject
            Renderer objRenderer = obj.GetComponent<Renderer>();

            // If the Renderer exists, change its material color
            if (objRenderer != null)
            {
                objRenderer.material.color = allStages[stageNumber].stageBallColor; // Change to the color you want
            }
        }
           
          //reset helix rotation
          transform.localEulerAngles = startRotation;

          //destroy old levels if there are any
          foreach(GameObject go in spawnedLevels)
          Destroy(go);

          //create new level/platform
          float levelDistance =  helixDistance / stage.level.Count;
          float spawnPosY = topTransform.localPosition.y;

          for (int i = 0; i < stage.level.Count; i++)
          {
            spawnPosY -= levelDistance;
            //create level within scene
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("levels"  + stage.level.Count + "spawned" );
            level.transform.localPosition = new Vector3(0, spawnPosY,0);
            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.level[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

             while (disabledParts.Count < partsToDisable)
             {
               GameObject randomParts = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
               if (!disabledParts.Contains(randomParts))
               {
                randomParts.SetActive(false);
                disabledParts.Add(randomParts);
               }
             }

             List<GameObject> leftParts = new List<GameObject>();
             foreach(Transform t in level.transform)
             {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if(t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
             }

             List<GameObject> deathParts = new List<GameObject>();
             while(deathParts.Count < stage.level[i].deathPartCount)
             {
                GameObject randomParts = leftParts[Random.Range(0, leftParts.Count)];
                  if (!deathParts.Contains(randomParts))
                  {
                    randomParts.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomParts);
                  }
             }
          }
    }
}
