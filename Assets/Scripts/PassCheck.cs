using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PassCheck : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
       GameManager.singleton.AddScore(2);

    foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
  {
    ball.GetComponent<BallController>().PlayPointUp();
    ball.GetComponent<BallController>().perfectPass++;

  }
       FindObjectOfType<CameraController>().TrueText();

    }
    
    }
