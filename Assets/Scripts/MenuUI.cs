using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text nameAndHighscore;

    private void Start()
    {
        LoadLastScore();
    }

    void LoadLastScore()
    {
        ScoreManager.instance.LoadScore();
        float score = ScoreManager.instance.highscore;
        string playerName = ScoreManager.instance.playerName;
        if (playerName != string.Empty)
        {
            nameAndHighscore.text = "Best Score : " + playerName + " : " + score;
        }
    }

    private void UpdateEmptyScore()
    {
        nameAndHighscore.text = "Best Score : " + " "+ " : " + " ";
    }

    void ReadInput()
    {
        // Read the text entered in the TMP Input Field
        string userInput = inputField.text;
        if ( userInput == string.Empty)
        {
            userInput = "UnknownName";
        }
        ScoreManager.instance.currentName = userInput;
    }

    public void StartNew()
    {
        ReadInput();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void DeleteSave()
    {
        ScoreManager.instance.DeleteScore();
        UpdateEmptyScore();
    }
}
