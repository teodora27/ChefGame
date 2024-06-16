using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tasta_sus_text;
    [SerializeField] private TextMeshProUGUI tasta_jos_text;
    [SerializeField] private TextMeshProUGUI tasta_st_text;
    [SerializeField] private TextMeshProUGUI tasta_dr_text;
    [SerializeField] private TextMeshProUGUI tasta_int_text;
    [SerializeField] private TextMeshProUGUI tasta_int_alt_text;
    [SerializeField] private TextMeshProUGUI tasta_pauza_text;

    private void Start()
    {
        InputJoc.Instanta.Cand_Rebind += InputJoc_Cand_Rebind;
        ManagerJoc.Instance.Cand_Starea_Se_Schimba += Instance_Cand_Starea_Se_Schimba;
        UpdateVisual();
        Show();
    }

    private void Instance_Cand_Starea_Se_Schimba(object sender, System.EventArgs e)
    {
        if(ManagerJoc.Instance.MergeNumaratoareaInversa())
        {
            Hide();
        }
    }

    private void InputJoc_Cand_Rebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        tasta_sus_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Sus);
        tasta_jos_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Jos);
        tasta_st_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Stanga);
        tasta_dr_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Dreapta);
        tasta_int_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Interactiune);
        tasta_int_alt_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.InteractiuneAlternativa);
        tasta_pauza_text.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Pauza);

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
