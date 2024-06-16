using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class NumaratoareInversaUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numaratoare_text;

    private Animator animator;
    private int numar_dinainte;
    private const string APARITIE_NUMERE = "aparitie_numere";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ManagerJoc.Instance.Cand_Starea_Se_Schimba += ManagerJoc_Cand_Starea_Se_Schimba;
        Hide();
    }

    private void ManagerJoc_Cand_Starea_Se_Schimba(object sender, System.EventArgs e)
    {
        if(ManagerJoc.Instance.MergeNumaratoareaInversa())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    
    private void Update()
    {
        int numar = Mathf.CeilToInt(ManagerJoc.Instance.GetNumaratoareInversaTimp());
        numaratoare_text.text =numar.ToString();
        if(numar!=numar_dinainte)
        {
            numar_dinainte = numar;
            animator.SetTrigger(APARITIE_NUMERE);
            SunetManager.Instance.PlayNumaratoareInversaSunet();
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
