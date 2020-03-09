using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    Pause
}

public class GameController : MonoBehaviour
{
    static private GameController _instance;

    private GameState state;

    private int score;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameState State
    {
        get
        {
            return state;
        }

        set
        {
            if (value == GameState.Pause)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }

            state = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            HUD.Instance.SetScore("Score: " +score.ToString() + "$");
        }
    }

    private void Awake()
    {
        state = GameState.Play;
        
        _instance = this;
    }

    public void Hit(IDestructable victim)
    {
        if (victim.GetType() == typeof(Enemy))
        {
            if (victim.Health > 0)
            {
                Score += 10;
            }
            else
            {
                Score += 50;
            }
        }
    }
}
