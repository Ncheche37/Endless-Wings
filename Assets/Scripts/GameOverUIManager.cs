using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using SQLite;

public class GameOverUIManager : MonoBehaviour
{
    
    public TMP_InputField playerNameInput;
   
    public TMP_Text scoreText;

    private int finalScore;


    public void SetupGameOverUI(int score)
    {
        finalScore = score;
        scoreText.text = "Score : " + finalScore.ToString();
    }

    public void OnSaveButtonClick()
    {
        string playerName = playerNameInput.text;

        if(!string.IsNullOrEmpty(playerName) )
        {
            

            SceneManager.LoadScene("SceneMenu");
            MusicManager.instance.PlayMenuMusic();
        }
    }
}
