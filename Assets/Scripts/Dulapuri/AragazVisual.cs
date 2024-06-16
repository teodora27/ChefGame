using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AragazVisual : MonoBehaviour
{
    [SerializeField] private Aragaz aragaz;
    [SerializeField] private GameObject lumina;
    [SerializeField] private GameObject particule;

    private void Start()
    {
        aragaz.Cand_Starea_Se_Schimba += Aragaz_Cand_Starea_Se_Schimba;
    }
    private void Aragaz_Cand_Starea_Se_Schimba(object sender, Aragaz.Cand_Starea_Se_SchimbaEventArgs e)
    {
        bool arata_visual = (e.stare == Aragaz.Stare.Gatire || e.stare == Aragaz.Stare.Gatit);
        lumina.SetActive(arata_visual);
        particule.SetActive(arata_visual);

    }
}
