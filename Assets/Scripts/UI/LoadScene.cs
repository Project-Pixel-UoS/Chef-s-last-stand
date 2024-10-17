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
        SceneManager.LoadScene(sceneName);
    }
}