using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    // Ref to the pause menu that will be set in the inspector.
    public GameObject pauseMenu;

    private void Update()
    {
        if (InputManager.instance.pauseInput && !PauseManager.instance.isPaused)
        {
            // Pause the game.
            Pause();
        }
        else if (InputManager.instance.resumeInputUI && PauseManager.instance.isPaused)
        {
            // Resume the game.
            Resume();
        }
    }

    // Pause the game.
    public void Pause()
    {
        PauseManager.instance.PauseGame();
        OpenPauseMenu();
    }

    // Resume the game.
    public void Resume()
    {
        PauseManager.instance.ResumeGame();
        ClosePauseMenu();
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

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MenuManager)), CanEditMultipleObjects]
public class MenuManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ReferenceChecks();
    }

    private void ReferenceChecks()
    {
        if (Selection.activeGameObject.GetComponent<MenuManager>().pauseMenu == null)
        {
            EditorGUILayout.HelpBox("Please set the Pause Menu!", MessageType.Error);
        }
    }
}
#endif
