using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateobjects : MonoBehaviour
{
    public GameObject ground;
  //  public GameObject bounds;
    public GameObject pickups;
    public GameObject player;
    public GameObject environs1;
    public GameObject environs2;

    public int numberOfObjects = 5;
    // public Vector3(float x, float y, float z); 
    Vector3 groundpos = new Vector3(-5.3f, 0.0f, -2.51f);
   // Vector3 boundpos = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 pickupspos = new Vector3(-13.31f, 1.55f, 2.11f);
    Vector3 pickupspos2 = new Vector3(-10.31f, 1.55f, 5.11f);
    Vector3 pickupspos3 = new Vector3(-7.31f, 1.55f, 6.11f);
    Vector3 environspos1 = new Vector3(-14.31f, 0f, 1.11f);
    Vector3 environspos2 = new Vector3(-1.31f, 0f, 10.11f);
    Vector3 playerpos = new Vector3(-12.23f, 0.0f, 3.07f);

    void Start()
    {

        
        Instantiate(ground, groundpos, Quaternion.identity);
        Instantiate(pickups, pickupspos, Quaternion.identity);
        Instantiate(pickups, pickupspos2, Quaternion.identity);
        Instantiate(pickups, pickupspos3, Quaternion.identity);
        Instantiate(environs1, environspos1, Quaternion.identity);
        Instantiate(environs2, environspos2, Quaternion.identity);

        //  Instantiate(environdeco, pos, Quaternion.identity);
        Instantiate(player, playerpos, Quaternion.identity);

    }
}
