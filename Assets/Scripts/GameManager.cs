using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int best;
    public int score;
    public int currentStage = 0;
    public static GameManager singleton;
    public GameObject ballPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if (singleton == null)
        singleton = this;
        else if (singleton != this)
        Destroy(gameObject);
     best = PlayerPrefs.GetInt("HighScore");   
    }

public void NextLevel()
{
  currentStage++;

  foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
  {
    ball.GetComponent<BallController>().ResetBall();
  }


  FindObjectOfType<HelixController>().LoadStage(currentStage);
}
public void RestartLevel()
{
singleton.score = 0;
FindObjectOfType<BallController>().ResetBall();
  FindObjectOfType<HelixController>().LoadStage(currentStage);

}
public void AddScore(int scoreToAdd)
{
    
    score += scoreToAdd;

    if(score > best)
    {
        best = score;
        PlayerPrefs.SetInt("HighScore", score);
    }
}
}
