using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MaterialProperty : MonoBehaviour
{
   // public GameObject sphere;
    MeshRenderer thisrend;
    public float Emissive;
    MaterialPropertyBlock block;


    // Update is called once per frame
    void Update()
    {
        thisrend = GetComponentInChildren<MeshRenderer>();
        if (thisrend?.sharedMaterial == null)
            return;

        var color = thisrend.sharedMaterial.GetColor("_EmissiveColorLDR");

        if (block == null)
        {
            block = new();

        }

        block.SetColor("_EmissiveColor", color * Emissive);
        thisrend.SetPropertyBlock(block);

        //thisrend.material.SetColor("_EmissiveColor", color*Emissive);
        //Debug.Log(color * Emissive);
    }
}
