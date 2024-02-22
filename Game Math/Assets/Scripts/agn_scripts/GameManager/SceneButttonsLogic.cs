using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonsLogic : MonoBehaviour
{
    //agn variable
    public static int agn_plot_scene_variable;

    //agnieszka plot scenes
    public void StartAgnPlotScene1()
    {
        SceneManager.LoadScene("Scene1_agn_plot");
        agn_plot_scene_variable = 1;
}

    public void StartAgnPlotScene2()
    {
        SceneManager.LoadScene("Scene2_agn_plot");
        agn_plot_scene_variable = 1;
    }

    public void StartAgnPlotScene3()
    {
        SceneManager.LoadScene("Scene3_agn_plot");
        agn_plot_scene_variable = 1;
    }

    public void StartAgnPlotScene4()
    {
        SceneManager.LoadScene("Scene4_agn_plot");
        agn_plot_scene_variable = 1;
    }

    public void StartAgnPlotScene5()
    {
        SceneManager.LoadScene("Scene5_agn_plot");
        agn_plot_scene_variable = 1;
    }

    //agnieszka endless scenes
    public void StartAgnEndlessScene1()
    {
        SceneManager.LoadScene("Scene1_agn");
        agn_plot_scene_variable = 0;
    }
    public void StartAgnEndlessScene2()
    {
        SceneManager.LoadScene("Scene2_agn");
        agn_plot_scene_variable = 0;
    }

    public void StartAgnEndlessScene3()
    {
        SceneManager.LoadScene("Scene3_agn");
        agn_plot_scene_variable = 0;
    }

    public void StartAgnEndlessScene4()
    {
        SceneManager.LoadScene("Scene4_agn");
        agn_plot_scene_variable = 0;
    }

    public void StartAgnEndlessScene5()
    {
        SceneManager.LoadScene("Scene5_agn");
        agn_plot_scene_variable = 0;
    }
}
