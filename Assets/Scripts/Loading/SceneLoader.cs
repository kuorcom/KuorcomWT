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
        StartCoroutine(LoadAsyncScene());
    }

    public IEnumerator LoadAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Main");
        asyncLoad.allowSceneActivation = false;

        loadingText.text = "LOADING...";

        while (!asyncLoad.isDone)
        {
            loadSlider.value = asyncLoad.progress;

            if (asyncLoad.progress >= 0.9f)
            {
                loadSlider.value = 1;
                loadingText.text = "Tap to Start";
                if (Input.GetMouseButtonDown(0))
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }

    }
}
