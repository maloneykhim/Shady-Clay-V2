

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    float currentProgress = 0;
    float maxProgress = 20;
    [SerializeField] Image progressBar;
    float decrement = 0.0005f;
    //0.002f
    float increment = 0.007f;

    public bool timeIsRunning = true;
    [SerializeField] float timeRemaining = 60f; // Set a default value
    [SerializeField] TMP_Text timeText;


    void Start()
    {
        if (timeRemaining <= 0) 
        {
            timeRemaining = 60f; // Ensure it starts at a valid time
        }
        timeIsRunning = true;
        UpdateProgressAmount();
        DisplayTime(timeRemaining);
    }
    
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            currentProgress += increment;
        }
        else
        {
            currentProgress -= decrement;
        }

        currentProgress = Mathf.Clamp(currentProgress, 0, maxProgress);
        UpdateProgressAmount();

        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeRemaining = Mathf.Clamp(timeRemaining, 0, Mathf.Infinity); // Ensure it never goes negative
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time ran out! Loading Lose_timeout scene...");
                timeIsRunning = false;
                SceneManager.LoadScene("Lose_timeout");
            }
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateProgressAmount()
    {
        progressBar.fillAmount = currentProgress / maxProgress;

        if (currentProgress >= maxProgress)
        {
            Debug.Log("You win! Loading Win01 scene...");
          
  
            SceneManager.LoadScene("Win01");

             
        }
    }
}

