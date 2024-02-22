using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputCollider : MonoBehaviour
{
    public GameObject invisibleObject;
    public TextMeshProUGUI randomNum;

    private void Start()
    {
        invisibleObject.SetActive(false);
    }

    void GenerateRandomValue()
    {
        int value = Random.Range(1, 2023);
        randomNum.text = value.ToString();
        //Debug.Log(randomNum.text);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InvisibleCollider"))
        {
            Destroy(collision.gameObject);
            GenerateRandomValue(); 
            invisibleObject.SetActive(true);
            StopPlayer();
        }
    }

    void StopPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            Animator playerAnimator = playerObject.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.enabled = false;
            }
            else
            {
                Debug.LogError("Animator not found");
            }

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

    public void HideObject()
    {
        invisibleObject.SetActive(false);
    }
}