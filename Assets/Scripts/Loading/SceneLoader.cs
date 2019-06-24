using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public UnityEngine.UI.Slider loadSlider;
    public UnityEngine.UI.Text loadingText;
    bool loaded = false;
    AsyncOperation asyncLoad;

    private void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        //
        StartCoroutine(LoadAsyncScene());
    }

    public IEnumerator LoadAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Main");

        loadingText.text = "LOADING...";

        while (!asyncLoad.isDone)
        {
            loadSlider.value = asyncLoad.progress;
            yield return null;
        }

    }
}
