using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayMaze()
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene("maze");
    }

}
