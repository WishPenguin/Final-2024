using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function to start level one
    public void PlayLevelOne()
    {
        SceneManager.LoadScene(1); // Load scene at index 1 for Level One
    }

    // Function to start level two
    public void PlayLevelTwo()
    {
        SceneManager.LoadScene(2); // Load scene at index 2 for Level Two
    }

    // Function to close the game
    public void Rest()
    {
        Application.Quit();

#if UNITY_EDITOR
        // If you want to stop playing the game in the editor, uncomment the next line
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
