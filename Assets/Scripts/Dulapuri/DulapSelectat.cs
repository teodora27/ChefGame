using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapSelectat : MonoBehaviour
{
    [SerializeField] private BazaDulap dulap;
    [SerializeField] private GameObject[] visual_dulap_vector;
    private void Start()
    {
        Jucator.Instanta.Cand_Dulapul_E_Selectat += Instanta_Cand_Dulapul_E_Selectat;
    }

    private void Instanta_Cand_Dulapul_E_Selectat(object sender, Jucator.Cand_Dulapul_E_SelectatEventArgs e)
    {
        if( e.dulap_selectat == dulap)
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
        foreach (GameObject visual_dulap in visual_dulap_vector)
        {
            visual_dulap.SetActive(true);
        }
            
    }
    private void Hide()
    {
        foreach (GameObject visual_dulap in visual_dulap_vector)
        {
            visual_dulap.SetActive(false);
        }
    }
}
