using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Singleton for easy ref elsewhere.
    public static PauseManager instance;

    public bool isPaused { get; private set; }

    private void Awake()
    {
        // Set singleton ref.
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        // Pause the game.
        isPaused = true;
        Time.timeScale = 0;

        // Change action map to completely disable player input.
        InputManager.playerInput.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        // Resume the game.
        isPaused = false;
        Time.timeScale = 1;

        // Change action map back to player so the player can resume playing the game.
        InputManager.playerInput.SwitchCurrentActionMap("Player");
    }
}
