using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState
{
    MainMenu,
    InGame,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("StartMenu");
    }


    public void StartGame()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene("Main");
    }

    public void Restart()
    {
        Debug.Log("Restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Quit()
    {
        Debug.Log("Quitting...");

        #if UNITY_EDITOR
                // Exit play mode in the Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Quit the application in a built game
                Application.Quit();
        #endif

        //Application.Quit();
    }


}
