using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool StartButton;
    public bool QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseUp()
    {
        if (StartButton)
        {
            SceneManager.LoadScene("Level1");
        }
        if (QuitButton)
        {
            Application.Quit();
        }
    }
}
