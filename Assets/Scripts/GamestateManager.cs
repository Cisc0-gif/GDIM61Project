using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;

    [SerializeField]
    private List<string> m_roomNames = new List<string>();

    public delegate void OnGamePause();
    public delegate void OnGameUnpause();
    public static OnGamePause onGamePause;
    public static OnGameUnpause onGameUnpause;

    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStateManager();
            }
            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;
    private GameStateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState) { return; }

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }

    public void Start()
	{
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
	}

    public void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            TogglePause();
		}
	}

    public void RoomTransition(int roomID)
	{
        //transition anim HERE
        if (_instance.m_roomNames.Count > 0)
        {
            SceneManager.LoadScene(_instance.m_roomNames[roomID]);
        }
	}

    public void ResetRoom()
    {
        CurrentGameState = GameState.GAMEPLAY;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TogglePause()
	{
        if (CurrentGameState == GameState.GAMEPLAY)
		{
            CurrentGameState = GameState.PAUSED;
            onGamePause();
            Time.timeScale = 0;
		}
        else
		{
            CurrentGameState = GameState.GAMEPLAY;
            onGameUnpause();
            Time.timeScale = 1;
        }
	}

}


[System.Serializable]
public enum GameState
{
    GAMEPLAY, PAUSED, DIALOUGE, TRANSITION
}
