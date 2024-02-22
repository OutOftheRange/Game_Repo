using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLogic2 : MonoBehaviour
{
    public static float timer = 60;
    public static bool storyMode = true;

    //olek story mode
    public void LoadPlotLevel1()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene1_ole_plot");
        storyMode = true;
    }

    public void LoadPlotLevel2()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene2_ole_plot");
        storyMode = true;
    }

    public void LoadPlotLevel3()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene3_ole_plot");
        storyMode = true;
    }

    public void LoadPlotLevel4()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene4_ole_plot");
        storyMode = true;
    }

    public void LoadPlotLevel5()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene5_ole_plot");
        storyMode = true;
    }

    //olek endless mode
    public void LoadEndlessLevel1()
    {
        SceneManager.LoadScene("Scenes/ole_scenes/Chapter 1");
        storyMode = false; 
    }

    public void LoadEndlessLevel2()
    {
        SceneManager.LoadScene("Scenes/ole_scenes/Chapter 2");
        storyMode = false;
    }

    public void LoadEndlessLevel3()
    {
        SceneManager.LoadScene("Scenes/ole_scenes/Chapter 3");
        storyMode = false;
    }

    public void LoadEndlessLevel4()
    {
        SceneManager.LoadScene("Scenes/ole_scenes/Chapter 4");
        storyMode = false;
    }

    public void LoadEndlessLevel5()
    {
        SceneManager.LoadScene("Scenes/ole_scenes/Chapter 5");
        storyMode = false;
    }
}
