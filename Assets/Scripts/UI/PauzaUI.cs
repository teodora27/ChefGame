using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauzaUI : MonoBehaviour
{
    public static PauzaUI instance { get; private set; }
    [SerializeField] private Button buton_meniu;
    [SerializeField] private Button buton_resume;
    [SerializeField] private Button buton_optiuni;

    private void Awake()
    {
        buton_resume.onClick.AddListener(() =>
        {
            ManagerJoc.Instance.Pauza();
        });
        buton_meniu.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Meniu);
        });
        buton_optiuni.onClick.AddListener(() =>
        {
            OptiuniUI.instance.Show();
            Hide();
        });
    }

    private void Start()
    {
        ManagerJoc.Instance.Cand_Pune_Pauza += ManagerJoc_Cand_Pune_Pauza;
        ManagerJoc.Instance.Cand_Iese_Din_Pauza += ManagerJoc_Cand_Iese_Din_Pauza;
        Hide();
    }

    private void ManagerJoc_Cand_Iese_Din_Pauza(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void ManagerJoc_Cand_Pune_Pauza(object sender, System.EventArgs e)
    {
        Show();
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
