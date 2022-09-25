using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    private int lives;
    private float Lives;
    private int level;
    private int score;
    private int resetlives = 0;

    public Text livesremainingText;
    public Text scoretext;



    private void Start()
    {   
        DontDestroyOnLoad(gameObject);
        NewGame();
        liveCountText();
       
    }

    private void NewGame()
    {
        lives = 15;
       // Lives = 15;
        score = 0;

        LoadLevel(2);
    }

    private void LoadLevel(int index)
    {
        level = index;
        Camera cam = Camera.main;
        if(cam != null)
        {
            cam.cullingMask = 0;
        }

        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void LevelComplete()
    {
        score += 1000;
        liveCountText();
        int nextLevel = level + 1;

        if(nextLevel<SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            LoadLevel(2);
        }
    }
    public void LevelFailed()
    {
        lives--;
       // Lives--;
        liveCountText();
        Debug.Log(lives);
        if(lives <= 0)
        {
            SceneManager.LoadScene("Start");

        }
        else
        {
            LoadLevel(level);
        }
    }
    private void Update()
    {
      //  livesremainingText.text=Mathf.Round(Lives).ToString();

    }
    void liveCountText()
    {   Scene scene=SceneManager.GetActiveScene();
        if (scene.name == "Start")
        {
            livesremainingText.text = resetlives.ToString();
            scoretext.text = resetlives.ToString();
        }


        else
        {
            livesremainingText.text = lives.ToString();
            scoretext.text = score.ToString();
        }
    }
}
