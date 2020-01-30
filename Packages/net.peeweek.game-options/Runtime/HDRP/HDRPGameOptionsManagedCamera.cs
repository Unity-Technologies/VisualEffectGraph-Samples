#if GAME_OPTIONS_HDRP
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace GameOptionsUtility.HDRP
{
    [RequireComponent(typeof(HDAdditionalCameraData))]
    public class HDRPGameOptionsManagedCamera : MonoBehaviour
    {
        private void OnEnable()
        {
            HDRPCameraOption.AddCamera(GetComponent<HDAdditionalCameraData>());
        }

        private void OnDisable()
        {
            HDRPCameraOption.RemoveCamera(GetComponent<HDAdditionalCameraData>());
        }
    }
}
#endif