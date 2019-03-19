using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SampleLoader : MonoBehaviour
{
    [Header("Loading")]
    public Image FullScreenBlack;
    public Text SceneNameText;
    public string ChangeButtonName = "Submit";
    public float SceneChangeDuration = 60.0f;

    [Min(0.002f)]
    public float fadeDuration = 2.0f;

    [Header("Debug")]
    public GameObject DebugRoot;
    public Text FramerateText;

    int next = 0;
    bool fading;
    bool loading;
    float fadeTTL;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.sceneCountInBuildSettings > 1)
            StartCoroutine(LoadScene(0));
    }

    void UpdateDebug()
    {
        if (FramerateText == null)
            return;

        FramerateText.text =  (1.0f / GetSmoothDeltaTime()).ToString("F1");
    }

    Queue<float> deltaTimeSamples = new Queue<float>();
    const float smoothDeltaTimePeriod = 0.5f;

    float GetSmoothDeltaTime()
    {
        float time = Time.unscaledTime;
        deltaTimeSamples.Enqueue(time);

        if(deltaTimeSamples.Count > 1)
        {
            float startTime = deltaTimeSamples.Peek();
            float delta = time - startTime;

            float smoothDelta = delta / deltaTimeSamples.Count;

            if (delta > smoothDeltaTimePeriod)
                deltaTimeSamples.Dequeue();

            return smoothDelta;
        }
        else
            return Time.unscaledDeltaTime;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.F12) && DebugRoot != null)
            DebugRoot.SetActive(!DebugRoot.activeSelf);

        UpdateDebug();

        fadeTTL -= Time.unscaledDeltaTime;

        if (Input.anyKey || Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            fadeTTL = SceneChangeDuration;

        if (!loading && (Input.GetButton(ChangeButtonName) || Input.touchCount != 0 || (fadeTTL < 0.0f)))
        {
            int current = next;
            next = (next + 1) % (SceneManager.sceneCountInBuildSettings - 1);

            if(next != current)
                StartCoroutine(LoadScene(current, next));
        }
    }

    IEnumerator LoadScene(params int[] scene)
    {
        loading = true;

        int sceneToUnload = -1, sceneToLoad;

        if(scene.Length == 1)
        {
            sceneToLoad = scene[0];
        }
        else
        {
            sceneToUnload = scene[0];
            sceneToLoad = scene[1];
        }

        if(scene.Length == 2) // Need to unload
        {
            StartCoroutine(FadeInCoroutine());
            while (fading)
                yield return new WaitForEndOfFrame();

            AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneToUnload+1);
            while (!unload.isDone)
                yield return new WaitForEndOfFrame();

        }

        SceneNameText.text = "";

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad+1, LoadSceneMode.Additive);
        while (!load.isDone)
            yield return new WaitForEndOfFrame();

        SceneNameText.text = SceneManager.GetSceneAt(1).name;

        StartCoroutine(FadeOutCoroutine());
        while (fading)
            yield return new WaitForEndOfFrame();

        loading = false;
        fadeTTL = SceneChangeDuration;
        yield return null;
    }

    IEnumerator FadeInCoroutine()
    {
        fading = true;

        float alpha = 0.0f;
        while(alpha <= 1.0f)
        {
            alpha += Time.unscaledDeltaTime / fadeDuration;
            var color = FullScreenBlack.color;
            color.a = Mathf.Clamp01(alpha);
            FullScreenBlack.color = color;
            yield return new WaitForEndOfFrame();
        }

        fading = false;
        yield return null;
    }

    IEnumerator FadeOutCoroutine()
    {
        fading = true;
        float alpha = 1.0f;
        while (alpha >= 0.0f)
        {
            alpha -= Time.unscaledDeltaTime / fadeDuration;
            var color = FullScreenBlack.color;
            color.a = Mathf.Clamp01(alpha);
            FullScreenBlack.color = color;
            yield return new WaitForEndOfFrame();
        }

        fading = false;
        yield return null;
    }

}
