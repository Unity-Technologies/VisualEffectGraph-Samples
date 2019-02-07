using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInPlayMode : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

}
