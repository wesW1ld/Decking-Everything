using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource menuMusic;

    private void Awake()
    {
        // Ensure the game time is not frozen.
        Time.timeScale = 1;

        StartCoroutine(BeginMenuMusic());
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();

        // If in the editor, stop playing the game.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator BeginMenuMusic()
    {
        yield return new WaitForSeconds(5.5f);
        menuMusic.Play();
    }
}
