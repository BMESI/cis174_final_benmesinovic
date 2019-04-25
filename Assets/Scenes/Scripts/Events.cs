using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    float TimerString = Time.deltaTime;
    Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //   playerRigidbody.GetComponent<PlayerMovement>();

        /* 
         pseudo
         extra features -- if the player reaches a certain amount of pickups within a certain time, they get extra points

        time counter starts
        player picks up pckages
            add to count (points)
        if packages amount (points) is == max && time is < TIME_BONUS_Threshold
            add to total count  bonus points     // [ maybe try: points * (certain amount of points for every second under the threshold ] 
          
         */



    }
}
