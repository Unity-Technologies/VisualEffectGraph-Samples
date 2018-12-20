using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Lights : MonoBehaviour
{
    [Header("Generation")]
    public int lightCount = 10;
    public float radius = 1.0f;
    public Color lightColor;
    public float intensity = 5.0f;

    [SerializeField]
    private Light[] lights;

#if UNITY_EDITOR
    [ContextMenu("Create Lights")]
    void CreateLights()
    {
        if(lights != null && lights.Length > 0)
        {
            foreach(var light in lights)
            {
                DestroyImmediate(light.gameObject);
            }
        }

        lights = new Light[lightCount];
        for (int i = 0; i < lightCount; ++i)
        {
            var go = new GameObject("Light" + i);

            lights[i] = go.AddComponent(typeof(Light)) as Light;
            lights[i].type = LightType.Point;
            lights[i].color = lightColor;
            lights[i].range = 5.0f;
            lights[i].intensity = (lightCount + i) / 2.0f * intensity / (lightCount * lightCount);

            float angle = Mathf.PI + 2.0f * (i + 1) * Mathf.PI / lightCount;
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
            go.transform.eulerAngles = new Vector3(0, 0, (360 * i / (float)lightCount)-60);
        }
    }
#endif

    [Header("Runtime")]
    public float intensityScale = 1.0f;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Length; ++i)
            lights[i].intensity = Mathf.Max(0.0f, (lightCount + i) / 2.0f * Mathf.Sqrt(intensityScale) * intensity / (lightCount * lightCount));
    }
}
