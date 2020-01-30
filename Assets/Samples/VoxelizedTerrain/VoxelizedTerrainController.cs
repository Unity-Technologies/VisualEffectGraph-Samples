using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.EventSystems;

public class VoxelizedTerrainController : MonoBehaviour
{
    [Header("Camera")]
    public Camera ViewingCamera;
    public GameObject CameraRoot;
    public Vector2 CameraMinMaxDistance = new Vector2(1.0f,2.0f);
    public Vector2 CameraMinMaxHeight = new Vector2(0.0f, 1.0f);
    public float CameraMinDistanceToUpVector = 1.0f;

    public float PanSpeed = 0.01f;
    public float OrbitSpeed = 0.01f;
    public float ScaleSpeed = 0.01f;

    [Header("UI")]
    public GameObject UIPanelRoot;
    public Slider WaterElevationSlider;
    public Slider ElevationSlider;
    public Slider InputHeightMapScaleSlider;

    [Header("Visual Effect Configuration")]
    public VisualEffect VisualEffect;

    public Vector2 BasePosition = Vector2.zero;
    public Vector2 BaseWorldScale = Vector2.one;

    public Vector2 MinMaxWorldScale = new Vector2(0.1f, 5.0f);

    public Vector2 InputHeightLevel = new Vector2(0.1f, 5.0f);
    public Vector2 WaterElevationRange = new Vector2(0.1f, 1.0f);
    public Vector2 ElevationRange = new Vector2(0.2f, 1.0f);

    public ExposedProperty Position = "Position";
    public ExposedProperty WorldSize = "WorldSize";
    public ExposedProperty InputHeightMapScale = "Input HeightMap Scale";
    public ExposedProperty WaterElevation = "Water Elevation";
    public ExposedProperty Elevation = "Elevation";

    private Vector2 m_Position;
    private Vector2 m_WorldSize;

    private Vector2 mousePos;
    private int clicked;

    private void OnEnable()
    {
        if (SampleLoader.instance != null)
        {
            SampleLoader.instance.onMenuToggle += OnSamplesMenuToggle;
        }
    }

    private void OnDisable()
    {
        if (SampleLoader.instance != null)
        {
            SampleLoader.instance.onMenuToggle -= OnSamplesMenuToggle;
        }
    }

    bool menuVisible = false;
    void OnSamplesMenuToggle(bool visible)
    {
        menuVisible = visible;
        UIPanelRoot.SetActive(!visible);
    }

    private void Start()
    {
        m_Position = BasePosition;
        m_WorldSize = BaseWorldScale;
        mousePos = Input.mousePosition;
        clicked = -1;
    }

    private void Update()
    {
        if (menuVisible)
            return;

        // Mouse Management
        Vector2 delta = (Vector2)Input.mousePosition - mousePos;
        mousePos = Input.mousePosition;
        Vector3 worldScaleVector = delta.x * ViewingCamera.transform.right + delta.y * ViewingCamera.transform.forward;

        if (CheckParameters())
        {
            if(clicked == -1)
            {
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                    clicked = 0;
                else if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
                    clicked = 1;
            }
            else // Manage Click
            {
                if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                    clicked = -1;
                else
                {
                    if (clicked == 0) // Pan/Scale
                    {
                        var planeVector = new Vector2(worldScaleVector.x, worldScaleVector.z);
                        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            float oldSize = m_WorldSize.x;
                            float newSize = Mathf.Clamp(ScaleSpeed * delta.y + oldSize, MinMaxWorldScale.x, MinMaxWorldScale.y);
                            m_WorldSize = new Vector2(newSize, newSize);
                        }
                        else
                        {
                            m_Position += planeVector * (PanSpeed / m_WorldSize.x);
                        }

                    }
                    else if (clicked == 1) // Orbit
                    {
                        float distance = (ViewingCamera.transform.position - CameraRoot.transform.position).magnitude;
                        ViewingCamera.transform.position -= OrbitSpeed * delta.x * ViewingCamera.transform.right + OrbitSpeed * delta.y * ViewingCamera.transform.up;

                        Vector3 direction = (ViewingCamera.transform.position - CameraRoot.transform.position).normalized;
                        ViewingCamera.transform.position = CameraRoot.transform.position + distance * direction;
                        
                        float height = Mathf.Clamp(ViewingCamera.transform.position.y, CameraMinMaxHeight.x, CameraMinMaxHeight.y);
                        Vector2 upAxisOffset = new Vector2(ViewingCamera.transform.position.x, ViewingCamera.transform.position.z);

                        upAxisOffset = upAxisOffset.normalized * Mathf.Max(upAxisOffset.magnitude, CameraMinDistanceToUpVector);

                        ViewingCamera.transform.position = new Vector3(upAxisOffset.x, height, upAxisOffset.y);
                    }
                }
            }


            float dist = (ViewingCamera.transform.position - CameraRoot.transform.position).magnitude;
            Vector3 dir = (ViewingCamera.transform.position - CameraRoot.transform.position).normalized;

            if (Input.mouseScrollDelta.y != 0)
            {

                dist += Input.mouseScrollDelta.y * 0.1f;

            }

            dist = Mathf.Clamp(dist, CameraMinMaxDistance.x, CameraMinMaxDistance.y);
            ViewingCamera.transform.position = CameraRoot.transform.position + dist * dir;

            VisualEffect.SetVector2(Position, m_Position);
            VisualEffect.SetVector2(WorldSize, m_WorldSize);

            // Sliders
            float inputHeightMapScale = Mathf.Lerp(InputHeightLevel.x, InputHeightLevel.y, InputHeightMapScaleSlider.value);
            float elevation = Mathf.Lerp(ElevationRange.x, ElevationRange.y, ElevationSlider.value);
            float waterElevation = Mathf.Lerp(WaterElevationRange.x, WaterElevationRange.y, WaterElevationSlider.value);

            CameraRoot.transform.position = new Vector3(CameraRoot.transform.position.x, waterElevation, CameraRoot.transform.position.z);
            ViewingCamera.transform.LookAt(CameraRoot.transform);

            VisualEffect.SetFloat(InputHeightMapScale, inputHeightMapScale);
            VisualEffect.SetFloat(Elevation, elevation);
            VisualEffect.SetFloat(WaterElevation, waterElevation);
        }
    }

    private bool CheckParameters()
    {
        return CameraRoot != null &&
            ViewingCamera != null &&
            ElevationSlider != null &&
            InputHeightMapScaleSlider != null &&
            WaterElevationSlider != null &&
            VisualEffect != null &&
            VisualEffect.HasVector2(Position) &&
            VisualEffect.HasVector2(WorldSize) &&
            VisualEffect.HasFloat(InputHeightMapScale) &&
            VisualEffect.HasFloat(WaterElevation) &&
            VisualEffect.HasFloat(Elevation);
    }
}
