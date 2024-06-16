using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticManager : MonoBehaviour
{
    private void Awake() // pentru curatarea campurilor statice
    {
        DulapTaiere.ResetStaticData();
        BazaDulap.ResetStaticData();
        Gunoi.ResetStaticData();
    }
}
