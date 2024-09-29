using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int health = 3;

    public bool isGameOver { get; set; } = false;
    public bool wonGame { get; private set; } = false;

    // Audio sources.
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource lichBossMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Throws a warning, so commenting this out as I believe everything works fine without it.
            // DontDestroyOnLoad(gameObject); // Optional: Makes sure the object persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(bool won)
    {
        PlayerHealth.healthSet = false;
        wonGame = won;
        SceneManager.LoadScene("EndScreen");
    }

    public void PlayLichBossMusic()
    {
        gameMusic.Stop();
        lichBossMusic.Play();
    }

    public void EndLichBossMusic()
    {
        lichBossMusic.Stop();
        gameMusic.Play();
    }
}
// use GameManager.Instance.variableName to access
//STORE PUBLIC VARIABLES HERE
