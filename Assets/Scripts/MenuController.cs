using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // 添加这行以使用IEnumerator

public class MenuController : MonoBehaviour
{
    [Header("音效配置")]
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    [Header("过渡效果")]
    public Animator sceneTransition;
    public float transitionTime = 1f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        PlayButtonSound();
        StartCoroutine(LoadSceneWithTransition("MainScene"));
    }

    public void QuitGame()
    {
        PlayButtonSound();
        Debug.Log("退出游戏");
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // 修正这里：将"lEnumerator"改为"IEnumerator"
    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        if (sceneTransition != null)
        {
            sceneTransition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
        SceneManager.LoadScene(sceneName);
    }

    private void PlayButtonSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void ShowHelpPanel(GameObject helpPanel)
    {
        PlayButtonSound();
        helpPanel.SetActive(true);
    }

    public void HideHelpPanel(GameObject helpPanel)
    {
        PlayButtonSound();
        helpPanel.SetActive(false);
    }

    public void ShowCreditsPanel(GameObject creditsPanel)
    {
        PlayButtonSound();
        creditsPanel.SetActive(true);
    }

    public void HideCreditsPanel(GameObject creditsPanel)
    {
        PlayButtonSound();
        creditsPanel.SetActive(false);
    }
}