using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    // Win and lose screen.
    [Header("Win/Lose Screens")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [Header("Win/Lose First Buttons")]
    [SerializeField] private GameObject winFirstButton;
    [SerializeField] private GameObject loseFirstButton;

    private void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        
        if (GameManager.Instance.wonGame)
        {
            winScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(winFirstButton);
        }
        else
        {
            loseScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(loseFirstButton);
        }
    }

    // Play the game again.
    public void PlayAgain()
    {
        // Will change this to input the level the player will play again.
        // Currenty is the scene marked with a "1" in the build settings.
        SceneManager.LoadScene(1);
    }

    // Load the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();

        // If in the editor, stop playing the editor application.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
