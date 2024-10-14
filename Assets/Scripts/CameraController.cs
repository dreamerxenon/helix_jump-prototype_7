using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // List of GameObjects tagged as "Ball"
    private List<GameObject> ballObjects = new List<GameObject>();
    
    // Keep track of the current object being followed
    private int currentIndex = 0;

    // A reference to the specific collider you want to detect collisions with
    public Collider targetCollider;

    private float offset;
    public GameObject ballPrefab;
   public TextMeshProUGUI awesomeText;
      public TextMeshProUGUI doubleBallText;
       public Vector3 startPos;



    void Awake()
    {
        // Initialize the list to handle dynamic ball additions
        ballObjects = new List<GameObject>();
        
        // Instantiate the first ball at the start of the game
        InstantiateFirstBall(new Vector3(0, 5.5f, -1.5f));
                FindObjectOfType<HelixController>().LoadStage(0);

    }

    // Update is called once per frame
    void Update()
    {
        // Ensure there is at least one object in the list and the index is valid
        if (ballObjects.Count > 0 && currentIndex < ballObjects.Count)
        {
            // Get the current GameObject (Ball)
            GameObject currentBall = ballObjects[currentIndex];

            if (currentBall != null)
            {
                // Update the camera's position based on the current ball's height
                Vector3 currPos = transform.position;
                currPos.y = currentBall.transform.position.y + offset;
                transform.position = currPos;
            }
        }

    }

    // This method is called when one of the balls collides with the specific collider
    public void OnBallCollision(GameObject ball)
    {
        // Check if the collided ball is the one being tracked
        if (ball == ballObjects[currentIndex])
        {
            // If there is only one ball, go to the next level
            if (ballObjects.Count == 1)
            {
                GameManager.singleton.NextLevel();
                        if (GameManager.singleton.currentStage == 2)
                    {
                      InstantiateFirstBall(new Vector3(0.5f, 5.5f, -1.5f));
                      doubleBallText.gameObject.SetActive(true);
                      Invoke("DissDoubleText", 1f);
                      
                    }

            }
            else
            {
                // Move to the next ball
                currentIndex++;
                
                // If all balls have collided, go to the next level
                if (currentIndex >= ballObjects.Count)
                {
                  Destroy(ballObjects[1]);
                    GameManager.singleton.NextLevel();
                    currentIndex = 0;
                    ballObjects.RemoveAt(1);
                    
                
            }
        }
        }
       
       
        
        
    }

    // Method to dynamically add a new GameObject to the list
    public void AddBall(GameObject newBall)
    {
        ballObjects.Add(newBall);

        // Update offset if this is the first ball
        if (ballObjects.Count == 1)
        {
            offset = transform.position.y - newBall.transform.position.y;
        }
    }

    // Method to instantiate the first ball
    public void InstantiateFirstBall(Vector3 spawnPosition)
    {

        // Instantiate the ball
        GameObject newBall = Instantiate(ballPrefab, spawnPosition, ballPrefab.transform.rotation);
        
        // Add the instantiated ball to the list
        AddBall(newBall);
    }

      public void TrueText()
  { 
        awesomeText.gameObject.SetActive(true);
       Invoke("FalseText", .5f);
       Debug.Log("awesome");


    }

    private void FalseText()
    {
        awesomeText.gameObject.SetActive(false);
    }
    private void DissDoubleText()
    {
        doubleBallText.gameObject.SetActive(false);
    }

}


