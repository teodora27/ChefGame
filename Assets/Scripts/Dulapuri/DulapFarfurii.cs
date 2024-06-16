using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapFarfurii : BazaDulap
{
    public event EventHandler Cand_Apar_Farfurii;
    public event EventHandler Cand_Dispar_Farfurii;


    [SerializeField] private MancareSO farfurieSO;

    private float spawn_farfurii_timp_max = 4f;
    private float spawn_farfurii_timp;
    private int farfurii;
    private int farfurii_max=4;
    private void Update()
    {
        spawn_farfurii_timp += Time.deltaTime;
        if(ManagerJoc.Instance.SeJoaca()&&spawn_farfurii_timp > spawn_farfurii_timp_max)
        {
            spawn_farfurii_timp = 0f;
            if(farfurii<farfurii_max)
            {
                farfurii++;
                Cand_Apar_Farfurii?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interactiune(Jucator jucator)
    {
        if(!jucator.AreObiect())
        {
            if(farfurii>0)
            {
                farfurii--;
                ObiecteBucatarie.SpawnObiect(farfurieSO, jucator);
                Cand_Dispar_Farfurii?.Invoke(this, EventArgs.Empty );

            }
        }
    }
}
