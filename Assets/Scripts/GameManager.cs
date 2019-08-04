using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        playing,
        won,
        died,
        startScreen,
        endScreen
    }

    public int[] Levels;
    public float RestartTimer;

    public int LevelIndex;

    public GameStates GameState;

    private float Timer;

    // Animator PanelAnim;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        GameState = GameStates.startScreen;
        Levels = new int[SceneManager.sceneCountInBuildSettings];
        // PanelAnim = GameObject.FindGameObjectWithTag("Panel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene()
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        LevelIndex++;
        levelIndex++;
        if (levelIndex < Levels.Length && GameState != GameStates.endScreen)
        {
            StartCoroutine(LoadAsyncScene(levelIndex, false));
        }
    }
    public void ReloadScene()
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadAsyncScene(levelIndex, true));
    }

    IEnumerator LoadAsyncScene(int _index, bool restart)
    {

        if (restart)
        {
            yield return new WaitForSeconds(RestartTimer);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_index);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // IEnumerator LoadSceneAfterAnim(int _index)
    // {
    //     PanelAnim.SetTrigger("SlideToRight");
    //     yield return new WaitForSeconds(0.666f);
    //     SceneManager.LoadScene(_index);
    // }
    public void Quit()
    {
        Application.Quit();
    }
}
