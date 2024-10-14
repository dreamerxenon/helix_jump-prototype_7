using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    private Rigidbody rb;
    public float impulseForce =5;
    private Vector3 startPos;
    public int perfectPass;
    public bool isSuperSpeedActive;
    public ParticleSystem pointUp;
    private CameraController cameraController;



    // Start is called before the first frame update
    void Awake()
    {
      rb = GetComponent<Rigidbody>();
      cameraController = FindObjectOfType<CameraController>();
      startPos = this.gameObject.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.GetComponent<Goal>()) // Set this to your specific collider's tag
        {
            // Notify the camera controller
            cameraController.OnBallCollision(gameObject);
        }
        if (ignoreNextCollision)
        return;

        if (isSuperSpeedActive)
        {
            if(!collision.gameObject.GetComponent<Goal>())
            {
              Destroy(collision.transform.parent.gameObject);
            }
        }else {
        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if (deathPart)
        deathPart.HitDeathPart();
        }

     rb.velocity = Vector3.zero;
     rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
     ignoreNextCollision = true;
     Invoke("AllowCollision", .2f);

     perfectPass = 0;
     isSuperSpeedActive = false;
   }

   private void Update()
   {
    if (perfectPass >= 3 && !isSuperSpeedActive)
    {
        rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
    }
   }

   private void AllowCollision()
   {
    ignoreNextCollision = false;
   }
public void ResetBall()
{
    this.gameObject.transform.position = startPos;
}
 public void PlayPointUp()
 {
  pointUp.Play();

 }

}
