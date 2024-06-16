using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemnalArdereUI : MonoBehaviour
{
    [SerializeField] private Aragaz aragaz;

    private void Start()
    {
        aragaz.Cand_Progresul_Se_Schimba += Aragaz_Cand_Progresul_Se_Schimba;
        Hide();
    }

    private void Aragaz_Cand_Progresul_Se_Schimba(object sender, InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs e)
    {
        float ardere_progres = .5f;
        bool show = aragaz.Se_Arde() && e.progres_normalized >= ardere_progres;
        if(show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
