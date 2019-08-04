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
        startScreen
    }

    public GameStates GameState;

    // Start is called before the first frame update
    void Start()
    {
        GameState = GameStates.startScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
