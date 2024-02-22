using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static int Gamemode;

    //dominika plot buttons
    public void ShowPlotChapter1()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene1_dom_plot");
        Gamemode = 0;
    }

    public void ShowPlotChapter2()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene2_dom_plot");
        Gamemode = 2;
    }

    public void ShowPlotChapter3()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene3_dom_plot");
        Gamemode = 0;
    }

    public void ShowPlotChapter4()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene4_dom_plot");
        Gamemode = 0;
    }

    public void ShowPlotChapter5()
    {
        SceneManager.LoadScene("Scenes/agn_scenes/PlotScenesFolder/Scene5_dom_plot");
        Gamemode = 0;
    }

    //dominika endless buttons
    public void ShowEndlessChapter1()
    {
        SceneManager.LoadScene("Scenes/dom_scenes/Chapter1&2/Chap1&2Game1");
        Gamemode = 1;
    }
    public void ShowEndlessChapter2()
    {
        SceneManager.LoadScene("Scenes/dom_scenes/Chapter1&2/Chap1&2Game1");
        Gamemode = 3;
    }
    public void ShowEndlessChapter3()
    {
        SceneManager.LoadScene("Scenes/dom_scenes/Chapter3/Chap3Game1");
        Gamemode = 1;
    }
    public void ShowEndlessChapter4()
    {
        SceneManager.LoadScene("Scenes/dom_scenes/Chapter4/Chap4Game1");
        Gamemode = 1;
    }
    public void ShowEndlessChapter5()
    {
        SceneManager.LoadScene("Scenes/dom_scenes/Chapter5/Chap5Game1");
        Gamemode = 1;
    }
}
