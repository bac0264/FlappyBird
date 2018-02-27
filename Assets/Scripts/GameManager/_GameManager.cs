using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour {

    public static _GameManager instance;
    // Use this for initialization
    private const string HIGH_SCORE = "High Score";
    void Awake()
    {
        _MakeSingleInstance();
        IsGameStartedForTheFirstTime();
    }
    void IsGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }
    }
    public void _MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void _setHighScore(int score) {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }
    public int _getHighScore() {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }
}
