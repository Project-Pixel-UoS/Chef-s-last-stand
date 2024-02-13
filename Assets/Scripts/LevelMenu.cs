using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void LevelSelect()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void PlayLevel1()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void PlayLevel3()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
