using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu; // Drag the PauseMenu GameObject here in the Inspector

    private void Start()
    {
        // Hide the pause menu at the start
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pause the game
            pauseMenu.SetActive(true); // Show the pause menu
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            pauseMenu.SetActive(false); // Hide the pause menu
        }
    }

    public void BackToMenu()
    {
        Debug.Log("Back to Menu button clicked!");
        Time.timeScale = 1; // Resume the game before loading the menu
        SceneManager.LoadScene("Menu Scene"); // Change to the main menu scene
    }
}