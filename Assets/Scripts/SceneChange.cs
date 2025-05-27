using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;



public class SceneChange : MonoBehaviour
{
    // Activates when is Called, from a button Preferrably
    public void LoadScene()
    {
        // Loads the listed Scene, may add functionality so that this same Script can be used for multiple buttons.
        SceneManager.LoadScene("SampleScene");

        // Shows a Log to test if this function Works.
        Debug.Log("Loading Level...");
        // Note to Self: Attach to Main Camera for Functionality, then add the scenes to "Build Settings"
    }

    // Same Conditions as Previous Function
    public void EndGame()
    {
        // Shows a Log to test if this function Works.
        Debug.Log("Exiting Game...");

        // Exits the Game App (May have No Function in the Editor)
        Application.Quit();
    }
}
