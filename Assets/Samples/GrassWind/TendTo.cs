using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TendTo : MonoBehaviour
{
    public GameObject Target;
    public GameObject LookAtTarget;

    public float TargetTendSpeed = 0.03f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.transform.position, TargetTendSpeed * Time.deltaTime);
        if (LookAtTarget != null)
            transform.LookAt(LookAtTarget.transform);
    }
}
