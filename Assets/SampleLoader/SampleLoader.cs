using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SampleLoader : MonoBehaviour
{
    public static SampleLoader instance;

    [Header("Loading")]
    public Image FullScreenBlack;
    public Text SceneNameText;
    public string ChangeButtonName = "Submit";

    [Header("Scene Loading")]
    [Min(0.002f)]
    public float fadeDuration = 2.0f;

    [Header("Scene Loading")]
    public Toggle DemoModeToggle;
    public float DemoModeSceneChangeDuration = 20.0f;
    public GameObject DemoModeProgressBG;
    public RectTransform DemoModeProgressBar;

    [Header("Menu")]
    public KeyCode MenuToggleKey = KeyCode.Escape;
    public RectTransform MenuTransform;
    public Button OpenMenuButton;
    public Button CloseMenuButton;

    [Header("Load Scene Window")]
    public GameObject LoadSceneWindowRoot;

    [Header("Options Window")]
    public GameObject OptionsWindowRoot;

    [Header("FPS Counter")]
    public GameObject DebugRoot;
    public Text FramerateText;
    public Toggle FPSCounterToggle;

    [Header("Screenshot")]
    public KeyCode ScreenshotCode = KeyCode.F10;

    int currentScene = 0;
    bool m_Fading;
    bool m_Loading;
    float m_FadeTTL;

    public event MenuOpenDelegate onMenuToggle;

    public delegate void MenuOpenDelegate(bool isOpen);
     

    void Start()
    {
        if (SceneManager.sceneCountInBuildSettings > 1)
            StartCoroutine(LoadScene(0));

        SetFPSVisible(false);
        SetMenuVisible(false);
        SetLoadSceneWindowVisible(false);
        SetOptionsWindowVisible(false);
        SetDemoMode(true);

        instance = this;
    }

    #region FPS COUNTER
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

    bool m_FPSVisible = false;

    public void SetFPSVisible(bool visible)
    {
        DebugRoot.SetActive(visible);
        m_FPSVisible = visible;
        FPSCounterToggle.isOn = visible;
    }

    public void MenuToggleFPSCounter()
    {
        SetFPSVisible(FPSCounterToggle.isOn);
    }

    #endregion

    #region MENU

    bool m_MenuVisible = false;

    public void SetMenuVisible(bool visible)
    {
        if(visible)
        {
            if(m_LoadSceneWindowVisible)
                SetLoadSceneWindowVisible(false);

            if (m_OptionsWindowVisible)
                SetOptionsWindowVisible(false);

            OpenMenuButton.gameObject.SetActive(false);
            MenuTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        else
        {
            OpenMenuButton.gameObject.SetActive(true);
            MenuTransform.anchoredPosition = new Vector3(MenuTransform.sizeDelta.x, 0, 0);
        }

        if(m_MenuVisible != visible)
        {
            m_MenuVisible = visible;

            if(onMenuToggle != null)
                onMenuToggle.Invoke(visible);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    #endregion

    #region DEMO MODE
    bool m_DemoMode = false;

    public void SetDemoMode(bool value)
    {
        m_DemoMode = value;
        DemoModeToggle.isOn = value;
        DemoModeProgressBG.SetActive(value);

        if (value)
            m_FadeTTL = DemoModeSceneChangeDuration;
    }

    public void MenuToggleDemoMode()
    {
        SetDemoMode(DemoModeToggle.isOn);
    }

    public void UpdateDemoMode()
    {
        // Pauses the demo mode when in the menu
        if (m_MenuVisible || m_LoadSceneWindowVisible || m_Loading || m_Fading)
            return;

        m_FadeTTL -= Time.unscaledDeltaTime;

        DemoModeProgressBar.localScale = new Vector3(1.0f - (m_FadeTTL / DemoModeSceneChangeDuration),1,1);

        if (Input.anyKey || Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            m_FadeTTL = DemoModeSceneChangeDuration;

        if (!m_Loading && m_FadeTTL < 0.0f)
            NextScene();
    }

    #endregion

    #region LOAD SAMPLE WINDOW

    bool m_LoadSceneWindowVisible = true;

    public void SetLoadSceneWindowVisible(bool value)
    {
        if (m_LoadSceneWindowVisible != value)
        {
            if (value)
            {
                SetOptionsWindowVisible(false);
                SetMenuVisible(false);
            }


            LoadSceneWindowRoot.SetActive(value);

            m_LoadSceneWindowVisible = value;

            if (onMenuToggle != null)
                onMenuToggle.Invoke(value);
        }
    }

    public void MenuLoadScene(int index)
    {
        SetLoadSceneWindowVisible(false);
        if(index >= 0)
            SwitchToScene(index);
    }

    #endregion

    #region OPTIONS WINDOW

    bool m_OptionsWindowVisible = true;

    public void SetOptionsWindowVisible(bool value)
    {
        if (m_OptionsWindowVisible != value)
        {
            if (value)
            {
                SetMenuVisible(false);
                SetLoadSceneWindowVisible(false);
            }


            OptionsWindowRoot.SetActive(value);

            m_OptionsWindowVisible = value;

            if (onMenuToggle != null)
                onMenuToggle.Invoke(value);
        }
    }

    #endregion
    void Update()
    {
        if (Input.GetKeyDown(MenuToggleKey))
        {
            SetLoadSceneWindowVisible(false);
            SetMenuVisible(!m_MenuVisible);
        }

        if (Input.GetKeyDown(KeyCode.F12))
            SetFPSVisible(!m_FPSVisible);

        if (Input.GetKeyDown(KeyCode.F11))
            SetDemoMode(!m_DemoMode);

        if (Input.GetKeyDown(KeyCode.F10))
        {
            var now = System.DateTime.Now;
            string filename = $"VisualEffectGraph-Samples-{SceneManager.GetSceneAt(1).name}-{now.Year}{now.Month.ToString("D2")}{now.Day.ToString("D2")}{now.Hour.ToString("D2")}{now.Minute.ToString("D2")}{now.Second.ToString("D2")}.png";
            Debug.Log($"Captured Screenshot to : {filename}");
            ScreenCapture.CaptureScreenshot(filename);
        }

        UpdateDebug();

        if(m_DemoMode)
            UpdateDemoMode();

        if ((Input.GetButton(ChangeButtonName) || Input.touchCount != 0))
        {
            NextScene();
        }
    }

    #region SCENE LOADING

    public void NextScene()
    {
        if (m_Loading || m_Fading)
            return;

        int previous = currentScene;
        currentScene = (currentScene + 1) % (SceneManager.sceneCountInBuildSettings - 1);

        if (currentScene != previous)
            StartCoroutine(LoadScene(previous, currentScene));
    }

    void SwitchToScene(int index)
    {
        int previous = currentScene;

        if(index != previous)
            StartCoroutine(LoadScene(previous, index));

        currentScene = index;
    }

    IEnumerator LoadScene(params int[] scene)
    {
        m_Loading = true;

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
            while (m_Fading)
                yield return new WaitForEndOfFrame();

            AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneToUnload+1);
            while (!unload.isDone)
                yield return new WaitForEndOfFrame();

        }

        // Disable menu / load window
        SetMenuVisible(false);
        SetLoadSceneWindowVisible(false);


        SceneNameText.text = "";

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad+1, LoadSceneMode.Additive);
        while (!load.isDone)
            yield return new WaitForEndOfFrame();

        SceneNameText.text = SceneManager.GetSceneAt(1).name;

        StartCoroutine(FadeOutCoroutine());
        while (m_Fading)
            yield return new WaitForEndOfFrame();

        m_Loading = false;
        m_FadeTTL = DemoModeSceneChangeDuration;
        yield return null;
    }

    IEnumerator FadeInCoroutine()
    {
        m_Fading = true;

        float alpha = 0.0f;
        while(alpha <= 1.0f)
        {
            alpha += Time.unscaledDeltaTime / fadeDuration;
            var color = FullScreenBlack.color;
            color.a = Mathf.Clamp01(alpha);
            FullScreenBlack.color = color;
            yield return new WaitForEndOfFrame();
        }

        m_Fading = false;
        yield return null;
    }

    IEnumerator FadeOutCoroutine()
    {
        m_Fading = true;
        float alpha = 1.0f;
        while (alpha >= 0.0f)
        {
            alpha -= Time.unscaledDeltaTime / fadeDuration;
            var color = FullScreenBlack.color;
            color.a = Mathf.Clamp01(alpha);
            FullScreenBlack.color = color;
            yield return new WaitForEndOfFrame();
        }

        m_Fading = false;
        yield return null;
    }

    #endregion

}
