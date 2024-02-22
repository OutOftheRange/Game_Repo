using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonStartScene : MonoBehaviour
{
    //animation variables
    public float transitionTime = 2f;
    public GameObject transitionObject; 

    void SceneChange()
    {
        SceneManager.LoadScene(NextScene());
    }

    string NextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "Scene1_agn_plot":
                return "Scene1_agn";
            case "Scene2_agn_plot":
                return "Scene2_agn";
            case "Scene3_agn_plot":
                return "Scene3_agn";
            case "Scene4_agn_plot":
                return "Scene4a_agn";
            case "Scene5_agn_plot":
                return "Scene5_agn";
            case "Scene1_ole_plot":
                return "Chapter 1";
            case "Scene2_ole_plot":
                return "Chapter 2";
            case "Scene3_ole_plot":
                return "Chapter 3";
            case "Scene4_ole_plot":
                return "Chapter 4";
            case "Scene5_ole_plot":
                return "Chapter 5";
            case "Scene1_dom_plot":
                return "Chap1&2Game1";
            case "Scene2_dom_plot":
                return "Chap1&2Game1";
            case "Scene3_dom_plot":
                return "Chap3Game1";
            case "Scene4_dom_plot":
                return "Chap4Game1";
            case "Scene5_dom_plot":
                return "Chap5Game1";
            default:
                return "WelcomeScene";
        }
    }

    void Start()
    {
        if (transitionObject == null)
        {
            transitionObject = GameObject.FindGameObjectWithTag("Transition");
        }

        if (transitionObject != null)
        {
            SpriteRenderer spriteRenderer = transitionObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = transitionObject.AddComponent<SpriteRenderer>();
            }

            Color colorSpriteRenderer = spriteRenderer.color;
            colorSpriteRenderer.a = 0f;
            spriteRenderer.color = colorSpriteRenderer;
        }
        else
        {
            Debug.LogError("Transition not found");
        }
    }

    public void GoToNextScene()
    {
        if (transitionObject != null)
        {
            SpriteRenderer spriteRenderer = transitionObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                StartCoroutine(ChangeAlpha(spriteRenderer, 0f, 1f, transitionTime));
            }
            else
            {
                Debug.LogError("SpriteRenderer not found");
            }
        }
        else
        {
            Debug.LogError("Transition not found");
        }

        Invoke("SceneChange", transitionTime);
    }

    IEnumerator ChangeAlpha(SpriteRenderer spriteRenderer, float alphaStart, float alphaEnd, float time)
    {
        float timeLeft = time;

        while (timeLeft > 0)
        {
            float alpha = Mathf.Lerp(alphaStart, alphaEnd, 1 - timeLeft / time);
            Color colorSpriteRenderer = spriteRenderer.color;
            colorSpriteRenderer.a = alpha;
            spriteRenderer.color = colorSpriteRenderer;

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        Color colorSpriteRendererFinal = spriteRenderer.color;
        colorSpriteRendererFinal.a = alphaEnd;
        spriteRenderer.color = colorSpriteRendererFinal;
    }
}
