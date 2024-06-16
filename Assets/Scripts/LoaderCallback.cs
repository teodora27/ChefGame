using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool e_primul_update = true;

    private void Update()
    {
        if(e_primul_update)
        {
            e_primul_update=false;
            Loader.LoaderCallback();
        }
    }
}
