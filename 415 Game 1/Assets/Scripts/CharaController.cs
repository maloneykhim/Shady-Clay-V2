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
    
    AudioController audioController;

   // private float minWaitTime = 2f;  // Minimum wait time before triggering sculpting animation
   // private float maxWaitTime = 10f; // Maximum wait time before triggering sculpting animation
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
       audioController.ThemeStart();
        SetNextTriggerTime();
        StartCoroutine(CheckCaughtCondition());
        explosionAnimator.gameObject.SetActive(false);
    }

    void Update()
    {

        // Check if the mouse is over a UI element
              if (EventSystem.current.IsPointerOverGameObject())
        {
            // If it is, don't perform the gameplay action
            return;
        }

        if (Input.GetMouseButton(0))
        {
            
            Debug.Log("loop");
            isSabotaging = true;
            playerAnimator.SetBool("isSabotaging", true);
            

            // isExploding = true;
            explosionAnimator.gameObject.SetActive(true);
            explosionAnimator.SetBool("isExploding", true);

            
            audioController.PlayPlayerSFX();
            
        }
        else
        {
            isSabotaging = false;
            playerAnimator.SetBool("isSabotaging", false);

            // isExploding = false;
            explosionAnimator.gameObject.SetActive(false);
            explosionAnimator.SetBool("isExploding", false);
        }

        if (Time.time >= nextTriggerTime)
        {
         Debug.Log("trigger time");
            TriggerAnimation();
            
        }


    if (Input.GetMouseButtonDown(0)) // Detects first press only
    {
        audioController.MetalTune();
       // audioController.StopTheme();
        
    }

    if (Input.GetMouseButtonUp(0)) // Detects release
    {
        audioController.StopMetalTune();
        
    }

    }

    void TriggerAnimation()
    {
        isSculpting = true;
        rivalAnimator.SetBool("isSculpting", true);
        audioController.PlayRivalSFX();
        Invoke(nameof(StopAnimation), Random.Range(minDuration, maxDuration));
    }

    void StopAnimation()
    {
        isSculpting = false;
        rivalAnimator.SetBool("isSculpting", false);
        audioController.StopSFX();
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
                Debug.Log("Caught!");
                isCaught = true;
                playerAnimator.SetBool("isCaught", true);

                // Rival plays catching animation
                rivalAnimator.SetBool("isCatchingPlayer", true);

                // Stop catching animation
                //rivalAnimator.SetBool("isCatchingPlayer", false);

                yield return new WaitForSeconds(5f); // Delay before resetting

                SceneManager.LoadScene("Lose_caught"); // molly

                isCaught = false;
                playerAnimator.SetBool("isCaught", false);

              
            }
            yield return null;
        }
    }
    }

