using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JucatorSunet : MonoBehaviour
{
    private Jucator jucator;
    private float pasi_timer;
    private float pasi_timer_max=0.1f;
    private void Awake()
    {
        jucator = GetComponent<Jucator>();
    }
    private void Update()
    {
        pasi_timer -= Time.deltaTime;
        if(pasi_timer<0f)
        {
            pasi_timer = pasi_timer_max;

            if(jucator.Merge())
            {
                float volum = 1f;
                SunetManager.Instance.PlayPasiSunet(jucator.transform.position, volum);
       
            }
             }
    }
}
