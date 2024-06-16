using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgresUI : MonoBehaviour
{
    [SerializeField] private GameObject progres_gameobject;
    [SerializeField] private Image linie;
    private InterfataProgresUI progres;
    private void Start()
    {
        progres = progres_gameobject.GetComponent<InterfataProgresUI>();
        if(progres == null)
        {
            Debug.LogError(progres_gameobject + "nu are componenta InterfataProgresUI");
        }
        progres.Cand_Progresul_Se_Schimba += Interfata_Progres_Cand_Progresul_Se_Schimba;
        linie.fillAmount = 0f;
        Hide();
    }
    private void Interfata_Progres_Cand_Progresul_Se_Schimba(object sender, InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs e)
    {
        linie.fillAmount = e.progres_normalized;
        if(e.progres_normalized ==0f || e.progres_normalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
