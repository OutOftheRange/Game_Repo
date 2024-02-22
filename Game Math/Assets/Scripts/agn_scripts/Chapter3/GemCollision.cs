using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GemCollision : MonoBehaviour
{
    private bool gemCollision = false;
    private JsonChangeData modifyGameData;

    void Start()
    {
        modifyGameData = JsonChangeData.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gemCollision)
        {
            gemCollision = true;
            if (SceneButtonsLogic.agn_plot_scene_variable == 1)
            {
                modifyGameData.ModifyGameProgress("Chapter4_Level1", true);
            }
            SceneManager.LoadScene("EndScene_agn");
        }
    }
}
