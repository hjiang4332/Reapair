using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    void update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
