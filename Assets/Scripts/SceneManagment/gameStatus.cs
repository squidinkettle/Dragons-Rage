using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStatus : MonoBehaviour
{
    public bool pressed;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
            SceneManager.LoadScene("OVERWORLDMENU");
    }

    private void toOverworld()
    {
       
    }
}
