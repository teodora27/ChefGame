using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarfurieIconSingurUI : MonoBehaviour
{
    [SerializeField] private Image imagine;
    public void SetObiectSO(MancareSO obiectSO)
    {
        imagine.sprite = obiectSO.sprite;
    }
}
