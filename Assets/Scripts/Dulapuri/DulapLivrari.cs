using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapLivrari : BazaDulap
{
    public static DulapLivrari Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interactiune(Jucator jucator)
    {
        if(jucator.AreObiect())
        {
            if(jucator.GetObiect().IncearcaSaPuiInFarfurie(out Farfurie farfurie))
            {
                //daca jucatorul are o farfurie
                LivrariManager.Instanta.GataReteta(farfurie);
                jucator.GetObiect().Autodistrugere();
            }
        }
    }
}
