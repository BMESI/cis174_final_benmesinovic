using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    WaitForSecondsRealtime waitForSecondsRealtime;
    public float speed =0;
    float seed1 = .11f;
    float seed2 = .10f;
    float seed3 = .20f;
    Vector3 PlayerObjectSize;
    // rigid type for player body 
    private Rigidbody rb;
    private int count;
    public Text countText;
    public Text winText;
     public float upForce = 2500f;
    List<GameObject> GameObjects = new List<GameObject>();
    // var for start position
    Vector3 startPos;
    private void Start()
    {
    // Start() is code to execute on the very first frame, and F/Update() executes on every frame.
    // assign rigidness to model
       rb = GetComponent<Rigidbody>();
        // start position
        PlayerObjectSize = rb.transform.localScale;
        startPos = transform.position;
        count = 0;
        SetCountText();
        winText.text = "";
    }
    // Physics code goes here
    void FixedUpdate()
    {
        // player movements 
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        if (Input.GetKeyUp("space") && ( rb.transform.position.y  <= .6) ) // If the player is pressing the "space" key player will move up.
        {
            Vector3 explosionPos = transform.position;
            // Add a jump force
            //            rb.AddForce(0, upForce * Time.deltaTime, 0);
            rb.AddExplosionForce(400f, explosionPos, 200f, 3000.0F);
            if (rb.transform.position.y > .6) // If the player is pressing the "space" key player will move up.
            {
                rb.drag = 5;
                rb.mass = 10;
            }
            else
            {
                rb.drag = 0;
                rb.mass = 1;

            }


        }



    }
     void OnTriggerEnter(Collider other)
    {
        // collisions 
        if (other.gameObject.CompareTag("Pick Up"))
        {
            GameObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
            count = count + 2;
            SetCountText();
            MorphPlayer(seed1, seed2, seed3);
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            ExplodeMotion();
            GameObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if  (other.gameObject.CompareTag("Boundary")) 
        { /// when out of bounds -- reset text and player object
            winText.text = " TRY AGAIN!";
            count = 0;
            SetCountText();
            ResetGame();
        }
    }
    void SetCountText()
    {// set text and return player when winning condit
       countText.text = "Score: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You win!";
            countText.text = "Score: " + count.ToString();
            count = 0;
            ResetGame();
        }
    }
    void ResetGame()
    {
        // change to start point and then reset player scale and set all pickups active -- not perfect
        rb.transform.localScale = PlayerObjectSize;
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
    void MorphPlayer(float amt, float amt2, float amt3)
    {
        // change the player object when needed
        rb.transform.localScale += new Vector3(amt, amt2, amt3);
    }
    void ExplodeMotion()
    {
        // eplosion physics when red cubes are picked up
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 20);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(1000f, explosionPos, 200f, 3000.0F);
        }
    }
    IEnumerator Wait()
    {
        // none of this works??
        // should be able to wait five seconds and then clear text
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        winText.text = "";
        print(Time.time);
    }
    void DisableText()
    { // this works for clearing text after five secconds ??
        winText.enabled = false;
    }  
}
