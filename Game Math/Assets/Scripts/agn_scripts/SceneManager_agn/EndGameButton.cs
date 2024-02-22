using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameButton : MonoBehaviour
{
    public GameObject invisibleQuitStayPanel;

    private void Start()
    {
        invisibleQuitStayPanel.SetActive(false);
    
    }

    public void StayButton()
    {
        invisibleQuitStayPanel.SetActive(false);
        GameObject showObject = GameObject.FindWithTag("ShowObject");

        if (!(showObject != null && showObject.activeSelf))
        {
            ActivatePlayer();
            ActivateAllAnimations();
            GameObject playerObject = GameObject.FindWithTag("Player");
            Animator playerAnimator = playerObject.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("running", false);
            }
            else
            {
                Debug.LogError("Animator not found");
            }
        }
        ActivateBallsMovement();
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void ShowPanel()
    {
        invisibleQuitStayPanel.SetActive(true);
        StopPlayer();
        StopAllAnimations();
        StopBallsMovement();
    }

    private void StopBallsMovement()
    {
        GameObject[] objectsToStop = GameObject.FindGameObjectsWithTag("Trap");

        foreach (GameObject ballObject in objectsToStop)
        {
            BallBehaviour ballBehaviourScript = ballObject.GetComponent<BallBehaviour>();

            if (ballBehaviourScript != null)
            {
                ballBehaviourScript.enabled = false;
            }
        }
    }

    private void ActivateBallsMovement()
    {
        GameObject[] objectsToActivate = GameObject.FindGameObjectsWithTag("Trap");

        foreach (GameObject ballObject in objectsToActivate)
        {
            BallBehaviour ballBehaviourScript = ballObject.GetComponent<BallBehaviour>();

            if (ballBehaviourScript != null)
            {
                ballBehaviourScript.enabled = true;
            }
        }
    }

    private void StopAllAnimations()
    {
        Animator[] sceneAnimators = FindObjectsOfType<Animator>();
        foreach (Animator animator in sceneAnimators)
        {
            animator.enabled = false;
        }
    }

    private void ActivateAllAnimations()
    {
        Animator[] sceneAnimators = FindObjectsOfType<Animator>();
        foreach (Animator animator in sceneAnimators)
        {
            animator.enabled = true;
        }
    }

    void StopPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;

                Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = Vector2.zero;
                }
                else
                {
                    Debug.LogError("Rigidbody2D not found");
                }
            }
            else
            {
                Debug.LogError("PlayerMovement not found");
            }
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }

    void ActivatePlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerMovement not found");
            }

            Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
            }
            else
            {
                Debug.LogError("Rigidbody2D not found");
            }
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }
}
