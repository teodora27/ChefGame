using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DulapAprovizionare : BazaDulap
{
    public event EventHandler Cand_Jucatorul_Ia_Obiectul;

    [SerializeField] private MancareSO obiect_bucatarie;

    public override void Interactiune(Jucator jucator) //Interact
    {
        if (!jucator.AreObiect())
        {
            ObiecteBucatarie.SpawnObiect(obiect_bucatarie, jucator);
            
            Cand_Jucatorul_Ia_Obiectul?.Invoke(this, EventArgs.Empty);
        }
        

    }

    
}
