using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CeasUI : MonoBehaviour
{
    [SerializeField] private Image timer;

    private void Update()
    {
        timer.fillAmount = ManagerJoc.Instance.GetTimerJoc();
    }
}
