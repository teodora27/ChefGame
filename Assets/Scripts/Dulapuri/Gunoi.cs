using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunoi : BazaDulap
{
    public static event EventHandler Cand_Obiectul_E_Aruncat;

    new public static void ResetStaticData()
    {
        Cand_Obiectul_E_Aruncat = null;
    }

    public override void Interactiune(Jucator jucator)
    {
        if(jucator.AreObiect())
        {
            jucator.GetObiect().Autodistrugere();
            Cand_Obiectul_E_Aruncat?.Invoke(this, EventArgs.Empty);
        }
    }
}
