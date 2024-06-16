using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private enum Mod
    {
           LookAt,
           LookAtInverted,
           CameraInFata,
           CameraInFataInverted,
    }
    [SerializeField] private Mod mod;

    private void LateUpdate()
    {
        switch(mod)
        {
            case Mod.LookAt:
             transform.LookAt(Camera.main.transform);
                break;
            case Mod.LookAtInverted:
                Vector3 directieCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + directieCamera);
                break;
            case Mod.CameraInFata:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mod.CameraInFataInverted:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
    }
}
