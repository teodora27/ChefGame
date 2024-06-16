using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazaDulap : MonoBehaviour, InterfataObiectParent
{
    public static event EventHandler Cand_e_Asezat_Un_Obiect;

    public static void ResetStaticData()
    {
        Cand_e_Asezat_Un_Obiect = null;
    }

    [SerializeField] private Transform punctul_de_deasupra;

    private ObiecteBucatarie obiect;

    public virtual void Interactiune(Jucator jucator)
    {
        Debug.Log("Opa");
    }

    public virtual void InteractiuneAlternativa(Jucator jucator)
    {
        //Debug.Log("Opa2");
    }

    public Transform ObiectSeMuta()
    {
        return punctul_de_deasupra;
    }

    public void SetObiect(ObiecteBucatarie obiect)
    {
        this.obiect = obiect;
        if(obiect != null)
        {
            Cand_e_Asezat_Un_Obiect?.Invoke(this, EventArgs.Empty);
        }
    }
    public ObiecteBucatarie GetObiect()
    {
        return obiect;
    }
    public void ClearObiect()
    {
        obiect = null;
    }
    public bool AreObiect()
    {
        return obiect != null;
    }
}
