using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    
    [SerializeField] private float maxLaunchForce = 5f;
    [SerializeField] private float neededDistForMaxForce = 1f;
    [SerializeField] private float minLaunchDistance = .1f;

    public int numberOfShots {get; private set;} = 0;

    bool isAiming = false;
    Vector3 aimingBeginPoint;
    Vector3 startingPos;

    Rigidbody2D rb2 = null;
    LineRenderer lineRend = null;
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        lineRend = GetComponent<LineRenderer>();
        mainCam = Camera.main;

        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver)
            return;
            
        //if left click hold && !aiming
        if (Input.GetMouseButtonDown(0) && !isAiming)
        {
            //Save point
            aimingBeginPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            aimingBeginPoint.z = 0;

            //Set aiming = true
            isAiming = true;
            lineRend.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0) && isAiming) //else, when player releases
        {
            //Aiming vector 
            Vector3 aimVector = GetAimVector();

            //if enough aim distance
            if (aimVector.magnitude >= minLaunchDistance)
            {
                float force = GetCurrentLaunchForce(aimVector);

                //Launch Ball
                LaunchBall(aimVector.normalized * force);

                numberOfShots++;
            }

            //is aiming = false
            isAiming = false;
            lineRend.enabled = false;
        }

        //Draw line relative to ball
        if (isAiming)
        {
            //get current aim
            Vector3 aimVector = GetAimVector();

            lineRend.SetPositions(new Vector3[]{transform.position, transform.position - aimVector});
        }
            
    }

    void LaunchBall(Vector3 force) {
        rb2.AddForce(force, ForceMode2D.Impulse);
    }

    float GetCurrentLaunchForce(Vector3 aimVector) {

        float percent = Mathf.Clamp01(aimVector.magnitude / neededDistForMaxForce);
        
        return percent * maxLaunchForce;
    }

    Vector3 GetAimVector() {
        //Get point under mouse 
        Vector3 mousePoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0;

        Vector3 dir = aimingBeginPoint - mousePoint;

        float strength = Mathf.Clamp(dir.magnitude, 0, neededDistForMaxForce); 

        //Calculate vector to save point
        return dir.normalized * strength;
    }

    public void ResetToStart() {
        transform.position = startingPos;
        rb2.velocity = Vector3.zero;
        rb2.angularVelocity = 0;
    }

    
}
