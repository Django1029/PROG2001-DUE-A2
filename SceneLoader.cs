using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Loaded by scene name
    public void LoadSceneByName(string sceneName)
    {
        // Check if the scene exists
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            // Transition effects can be added before loading
            Debug.Log($"Loading scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"scenario {sceneName} does not exist£¡");
        }
    }

    // Loading via Scene Index 
    public void LoadSceneByIndex(int sceneIndex)
    {
        // Get the total number of scenes
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (sceneIndex >= 0 && sceneIndex < sceneCount)
        {
            Debug.Log($"Loading scene index: {sceneIndex}");
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"Invalid scene index: {sceneIndex} (Total number of scenes: {sceneCount})");
        }
    }

    // Reloads the current scene
    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Quit the app
    public void QuitApplication()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}