using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SampleLoader : MonoBehaviour
{
    [Header("Loading")]
    public string[] Playlist;
    public Image FullScreenBlack;
    public Text SceneNameText;
    public string ChangeButtonName = "Submit";
    public float SceneChangeDuration = 60.0f;

    [Min(0.002f)]
    public float fadeDuration = 2.0f;

    [Header("Debug")]
    public Text FramerateText;

    int index = 0;
    bool fading;
    bool loading;
    float fadeTTL;
    // Start is called before the first frame update
    void Start()
    {
        if (Playlist.Length > 0)
            StartCoroutine(LoadScene(Playlist[0]));
    }

    void UpdateDebug()
    {
        if (FramerateText == null)
            return;

        FramerateText.text =  (1.0f / Time.smoothDeltaTime).ToString("F1");

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        UpdateDebug();

        fadeTTL -= Time.unscaledDeltaTime;

        if (Input.anyKey || Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            fadeTTL = SceneChangeDuration;

        if (!loading && (Input.GetButton(ChangeButtonName) || Input.touchCount != 0 || (fadeTTL < 0.0f)))
        {
            int current = index;
            index = (index + 1) % Playlist.Length;
            StartCoroutine(LoadScene(Playlist[current], Playlist[index]));
        }
    }

    IEnumerator LoadScene(params string[] scene)
    {
        loading = true;

        string sceneToUnload = "", sceneToLoad;
        if(scene.Length == 1)
        {
            sceneToLoad = scene[0];
        }
        else
        {
            sceneToUnload = scene[0];
            sceneToLoad = scene[1];
        }

        if(scene.Length == 2)
        {
            StartCoroutine(FadeInCoroutine());
            while (fading)
                yield return new WaitForEndOfFrame();

            AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneToUnload);
            while (!unload.isDone)
                yield return new WaitForEndOfFrame();

        }

        SceneNameText.text = sceneToLoad;

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        while (!load.isDone)
            yield return new WaitForEndOfFrame();

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
