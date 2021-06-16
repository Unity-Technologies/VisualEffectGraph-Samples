using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


[ExecuteAlways]
[RequireComponent(typeof(HDAdditionalLightData))]

public class OutputEventHandlerLightUpdate : VFXOutputEventPrefabAttributeAbstractHandler
{
    public AnimationCurve brightnessScaleOverTime;
    public AnimationCurve volumetricScaleOverTime;
    public Gradient colorOverTime;

    float time;
    HDAdditionalLightData additionalLightData;

    private void OnEnable()
    {
        additionalLightData = GetComponent<HDAdditionalLightData>();

    }

    public override void OnVFXEventAttribute(VFXEventAttribute eventAttribute, VisualEffect visualEffect)
    {
        time = 0;

    }

    void Update()
    {
        time += Time.deltaTime;
        additionalLightData.lightDimmer = brightnessScaleOverTime.Evaluate(time);
        additionalLightData.volumetricDimmer = volumetricScaleOverTime.Evaluate(time);
        additionalLightData.color = colorOverTime.Evaluate(time);

    }
}
