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

    private int LevelIndex;

    public GameStates GameState;

    Animator PanelAnim;

    // Start is called before the first frame update
    void Start()
    {
        GameState = GameStates.startScreen;
        Levels = new int[SceneManager.sceneCountInBuildSettings];
        PanelAnim = GameObject.FindGameObjectWithTag("Panel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene()
    {
        LevelIndex++;
        if (LevelIndex < Levels.Length && GameState != GameStates.endScreen)
        {
            StartCoroutine(LoadAsyncScene(LevelIndex));
        }
    }
    public void ReloadScene()
    {
        StartCoroutine(LoadAsyncScene(LevelIndex));
    }

    IEnumerator LoadAsyncScene(int _index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_index);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
