using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; //molly 

public class CharaController : MonoBehaviour
{
    public Animator playerAnimator;

    public Animator rivalAnimator;

    public Animator explosionAnimator;

    public Animator starAnimator;
    
    AudioController audioController;

    private float minDuration = 1f;  // Minimum sculpting animation duration
    private float maxDuration = 5f;  // Maximum sculpting animation duration

    private float nextTriggerTime;
    private bool isSabotaging = false;
    private bool isSculpting = false;
    private bool isCaught = false;
   // private bool isExploding = false;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    void Start()
    {
       audioController.StartTheme();
        SetNextTriggerTime();
        StartCoroutine(CheckCaughtCondition());
        explosionAnimator.gameObject.SetActive(false);
        starAnimator.gameObject.SetActive(false);
    }

    void Update()
    {

        // Check if the mouse is over a UI element
              if (EventSystem.current.IsPointerOverGameObject())
        {
            // If it is, don't perform the gameplay action
            return;
        }


    if(Input.GetMouseButton(0)){
         audioController.PlayPlayerSFX();
    }

        if (Input.GetMouseButtonDown(0)) // Detects first press
    {
       
        isSabotaging = true;
        playerAnimator.SetBool("isSabotaging", true);

        explosionAnimator.gameObject.SetActive(true);
        explosionAnimator.SetBool("isExploding", true);

       
        audioController.PauseTheme(); 
        audioController.MetalTune();  
    }

    if (Input.GetMouseButtonUp(0)) // Detects release
    {
        Debug.Log("Mouse Released - Resuming Theme");
        isSabotaging = false;
        playerAnimator.SetBool("isSabotaging", false);

        explosionAnimator.gameObject.SetActive(false);
        explosionAnimator.SetBool("isExploding", false);

        audioController.PauseMetalTune();
        audioController.StartTheme(); 
    }

        if (Time.time >= nextTriggerTime)
        {
         Debug.Log("trigger time");
            TriggerAnimation();
            
        }


    }

    void TriggerAnimation()
    {
        isSculpting = true;
        rivalAnimator.SetBool("isSculpting", true);
        starAnimator.gameObject.SetActive(true);
        audioController.PlayRivalSFX();
        Invoke(nameof(StopAnimation), Random.Range(minDuration, maxDuration));
    }

    void StopAnimation()
    {
        isSculpting = false;
        rivalAnimator.SetBool("isSculpting", false);
        starAnimator.gameObject.SetActive(false);
        audioController.StopRivalSFX();
        SetNextTriggerTime();
    }

    void SetNextTriggerTime()
    {
        int range = Random.Range(1,3);
        //nextTriggerTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
        nextTriggerTime = Time.time + (4f*range);
        Debug.Log(nextTriggerTime);
    }

    IEnumerator CheckCaughtCondition()
    {
        while (true)
        {
            if (isSabotaging && isSculpting && !isCaught)
            {
                //audioController.CaughtSFX();
                audioController.StopAudio(); 
            
                Debug.Log("Caught!");
                isCaught = true;
                playerAnimator.SetBool("isCaught", true);
                

                // Rival plays catching animation
                rivalAnimator.SetBool("isCatchingPlayer", true);

                // Stop catching animation
                //rivalAnimator.SetBool("isCatchingPlayer", false);

                yield return new WaitForSeconds(3f); // Delay before resetting

                 

                SceneManager.LoadScene("Lose_caught"); // molly
              
                isCaught = false;
                playerAnimator.SetBool("isCaught", false);

              
            }
            yield return null;
        }
    }
    }

