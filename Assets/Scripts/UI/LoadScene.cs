using System.Text.RegularExpressions;
using Music;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    /// <summary>
    /// This method is referenced by different buttons
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SoundPlayer.instance.PlayButtonClickFX();
        string previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        
        if (CheckPreviousSceneWasLevel(previousScene))
        {
            GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Play();
        }
    }

    private bool CheckPreviousSceneWasLevel(string previousScene)
    {
        return previousScene.ContainsInsensitive("Level") && char.IsDigit(previousScene[^1]);
    }
}