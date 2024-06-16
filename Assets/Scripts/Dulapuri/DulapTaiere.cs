using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DulapTaiere : BazaDulap, InterfataProgresUI
{
    public static event EventHandler Cand_Taie_Ceva; // e folosit pentru sunete

    new public static void ResetStaticData()
    {
        Cand_Taie_Ceva = null;
    }

    public event EventHandler<InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs> Cand_Progresul_Se_Schimba;

    public event EventHandler Cand_Taie; //e folosit pt visual, sa miste cutitul

    [SerializeField] private TaiereSO[] taiere_vector;

    private int progres_taiere;

    public override void Interactiune(Jucator jucator)
    {
        if (!AreObiect())//nu e niciun obiect pe dulap
        {
            if (jucator.AreObiect())
            {
                if (AreRetetaCuInput(jucator.GetObiect().GetMancareSO()))
                {
                    jucator.GetObiect().SetObiectParinte(this);
                    progres_taiere = 0;
                    TaiereSO taiereSO = Get_Reteta_Taiere(GetObiect().GetMancareSO());
                    Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                    {
                        progres_normalized = (float)progres_taiere / taiereSO.taiere_max
                    });
                }

            }

        }
        else // e ceva pe dulap
        {
            if (jucator.AreObiect())
            {
                if (jucator.GetObiect().IncearcaSaPuiInFarfurie(out Farfurie farfurie))
                {
                    //jucatorul duce o farfurie

                    if (farfurie.IncearcaAdaugaIngrediect(GetObiect().GetMancareSO()))
                    {
                        GetObiect().Autodistrugere();

                    }
                }
            }
            else
            {
                GetObiect().SetObiectParinte(jucator);
            }
        }
    }

    public override void InteractiuneAlternativa(Jucator jucator)
    {
        if (AreObiect() && AreRetetaCuInput(GetObiect().GetMancareSO())) //se taie daca exsita un obiet si nu e deja taiat
        {
            progres_taiere++;
            Cand_Taie?.Invoke(this, EventArgs.Empty);
            Cand_Taie_Ceva?.Invoke(this, EventArgs.Empty);

            TaiereSO taiereSO = Get_Reteta_Taiere(GetObiect().GetMancareSO());

            Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
            {
                progres_normalized = (float)progres_taiere / taiereSO.taiere_max
            });

            if (progres_taiere >= taiereSO.taiere_max)
            {
                MancareSO output_mancareSO = GetFelii(GetObiect().GetMancareSO());
                GetObiect().Autodistrugere();

                ObiecteBucatarie.SpawnObiect(output_mancareSO, this);
            }


        }
    }

    private bool AreRetetaCuInput(MancareSO inputSO)
    {
        TaiereSO taiereSO = Get_Reteta_Taiere(inputSO);
        return taiereSO != null;

    }

    private MancareSO GetFelii(MancareSO input_mancareSO) //GetOutputFromInput
    {
        TaiereSO taiereSO = Get_Reteta_Taiere(input_mancareSO);
        if (taiereSO != null)
        {
            return taiereSO.output;
        }
        else
        {
            return null;
        }

    }
    private TaiereSO Get_Reteta_Taiere(MancareSO input_mancareSO) //GetCuttingRecipeSOWithInput
    {
        foreach (TaiereSO taiereSO in taiere_vector)
        {
            if (taiereSO.input == input_mancareSO)
            {
                return taiereSO;
            }
        }
        return null;
    }

}
