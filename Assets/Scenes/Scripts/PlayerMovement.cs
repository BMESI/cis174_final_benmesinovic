using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
  // public Time TimerString2  = Time.deltaTime;// time function



    public List<GameObject> GameObjects = new List<GameObject>(); // this needs to be set to public for some of the mthods to able to access it....
    private int count;
    public Text countText;
    public float TimerString = Time.deltaTime;
    public string timerString;

    public Text winText;
    public float upForce = 2500f;
    Vector3 startPos;

    public void Start()
    {
        count = 0;
        SetCountText();
        winText.text = "";
    }

    public void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        startPos = playerRigidbody.position;
    }
    public void Update()
    {

        if (count == 8)
        {
            ResetGame();
            SetCountText();
        }
        if (Input.GetKeyUp("space"))
        //&& (playerRigidbody.transform.position.y <= .6)) // If the player is pressing the "space" key player will move up.
        {


            Vector3 explosionPos = transform.position;


            // the cube is going to move upwards in 10 units per second
            playerRigidbody.velocity = new Vector3(1, 10, 10);
            Debug.Log("jump");
            playerRigidbody.AddExplosionForce(400f, explosionPos, 200f, 3000.0F);

            // Add a jump force
            //            rb.AddForce(0, upForce * Time.deltaTime, 0);
            // playerRigidbody.AddExplosionForce(400f, explosionPos, 200f, 3000.0F);
            // Debug.Log("Spaced or jumped");

            /* if (playerRigidbody.transform.position.y < .6) // If the player is pressing the "space" key player will move up.
             {
                 Debug.Log("1st if reached");
                 playerRigidbody.drag = 5;
                 playerRigidbody.mass = 10;
             }
             else
             {
                 Debug.Log("else reached");

                 playerRigidbody.drag = 0;
                 playerRigidbody.mass = 1;

             }
             */

        }
    }

    public void FixedUpdate()
    {
        timerString = TimerString.ToString();
        Debug.Log(timerString);
        if ( count == 8)
        {
            SceneManager.LoadScene("SendScore");
            ResetGame();
            SetCountText();
        }
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v);

        if (Input.GetKeyUp("space") )
            //&& (playerRigidbody.transform.position.y <= .6)) // If the player is pressing the "space" key player will move up.
        {

            
            Vector3 explosionPos = transform.position;


            // the cube is going to move upwards in 10 units per second
            playerRigidbody.velocity = new Vector3(1, 10, 10);
                Debug.Log("jump");
            

            // Add a jump force
            //            rb.AddForce(0, upForce * Time.deltaTime, 0);
           // playerRigidbody.AddExplosionForce(400f, explosionPos, 200f, 3000.0F);
           // Debug.Log("Spaced or jumped");

            /* if (playerRigidbody.transform.position.y < .6) // If the player is pressing the "space" key player will move up.
             {
                 Debug.Log("1st if reached");
                 playerRigidbody.drag = 5;
                 playerRigidbody.mass = 10;
             }
             else
             {
                 Debug.Log("else reached");

                 playerRigidbody.drag = 0;
                 playerRigidbody.mass = 1;

             }
             */

        }

        if (Input.GetKeyUp("escape" ) )
            {
            SceneManager.LoadScene("MainMenu2");

        }

    }
    public void OnTriggerEnter(Collider other)
    {
        // collisions 
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            GameObjects.Add(other.gameObject);

            count = count + 2;
            Debug.Log(count);

            SetCountText();
        }

        if (other.gameObject.CompareTag("Boundary"))
        { /// when out of bounds -- reset text and player object
            winText.text = " TRY AGAIN!";
            count = 0;
            SetCountText();
            ResetGame();
        }
        if (playerRigidbody.gameObject.tag == "Ground")
        {
            Debug.Log("grounded: " );

        }
    }
    public void SetCountText()
    {// set text and return player when winning condit
        countText.text = "Score: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You win!";
            winText.IsActive();
            countText.text = "Score: " + count.ToString();
            count = 0;
            ResetGame();
        }
 
    }
    public void ResetGame()
        {
            // change to start point and then reset player scale and set all pickups active -- not perfect
            transform.position = startPos;
            count = 0;
            SetCountText();
            // clear text after five seconds 
            Invoke("DisableText", 5f);
            // activate items again
            foreach (GameObject GOTypes in GameObjects)
            {
                GOTypes.gameObject.SetActive(true);
            }
        }

    public void DisableText()
        { // this works for clearing text after five secconds ??
            winText.enabled = false;
        }
    public void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    public void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
