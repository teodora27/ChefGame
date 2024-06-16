using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptiuniUI : MonoBehaviour
{
    public static OptiuniUI instance { get; private set; }
    [SerializeField] private Button buton_sunete;
    [SerializeField] private Button buton_muzica;
    [SerializeField] private Button buton_inchidere;
    [SerializeField] private TextMeshProUGUI sunete_text;
    [SerializeField] private TextMeshProUGUI muzica_text;

    [SerializeField] private Button buton_sus;
    [SerializeField] private Button buton_jos;
    [SerializeField] private Button buton_stg;
    [SerializeField] private Button buton_dr;
    [SerializeField] private Button buton_int;
    [SerializeField] private Button buton_int_alt;
    [SerializeField] private Button buton_pauza;

    [SerializeField] private TextMeshProUGUI text_sus;
    [SerializeField] private TextMeshProUGUI text_jos;
    [SerializeField] private TextMeshProUGUI text_stg;
    [SerializeField] private TextMeshProUGUI text_dr;
    [SerializeField] private TextMeshProUGUI text_int;
    [SerializeField] private TextMeshProUGUI text_int_alt;
    [SerializeField] private TextMeshProUGUI text_pauza;

    [SerializeField] private Transform apasa_pt_rebind;


    private void Awake()
    {
        instance = this;
        buton_sunete.onClick.AddListener(() =>
        {
            SunetManager.Instance.SchimbaVolumul();
            UpdateVisual();
        });
        buton_muzica.onClick.AddListener(() =>
        {
            MuzicManager.Instanta.SchimbaVolumul();
            UpdateVisual();
        });
        buton_inchidere.onClick.AddListener(() =>
        {
            Hide();
            ManagerJoc.Instance.Pauza();
        });
        buton_sus.onClick.AddListener(() => {RebindBinding(InputJoc.Binding.Sus);});
        buton_jos.onClick.AddListener(() => { RebindBinding(InputJoc.Binding.Jos); });
        buton_stg.onClick.AddListener(() => { RebindBinding(InputJoc.Binding.Stanga); });
        buton_dr.onClick.AddListener(() => { RebindBinding(InputJoc.Binding.Dreapta); });
        buton_int.onClick.AddListener(() => { RebindBinding(InputJoc.Binding.Interactiune); });
        buton_int_alt.onClick.AddListener(() => { RebindBinding(InputJoc.Binding.InteractiuneAlternativa); });
        buton_pauza.onClick.AddListener(() => { RebindBinding(InputJoc.Binding.Pauza); });

    }
    private void Start()
    {
        ManagerJoc.Instance.Cand_Iese_Din_Pauza += ManagerJoc_Cand_Iese_Din_Pauza;
        UpdateVisual();
        Hide();
        HideApasaPtRebind();
    }

    private void ManagerJoc_Cand_Iese_Din_Pauza(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        sunete_text.text ="Sunete:" + Mathf.Round(SunetManager.Instance.GetVolum() * 10f);
        muzica_text.text = "Muzica:" + Mathf.Round(MuzicManager.Instanta.GetVolum() * 10f);


        text_sus.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Sus);
        text_jos.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Jos);
        text_stg.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Stanga);
        text_dr.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Dreapta);
        text_int.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Interactiune);
        text_int_alt.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.InteractiuneAlternativa);
        text_pauza.text = InputJoc.Instanta.GetBindingText(InputJoc.Binding.Pauza);


    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowApasaPtRebind()
    {
        apasa_pt_rebind.gameObject.SetActive(true);
    }
    public void HideApasaPtRebind()
    {
        apasa_pt_rebind.gameObject.SetActive(false);
    }

    private void RebindBinding(InputJoc.Binding binding)
    {
        ShowApasaPtRebind();
        InputJoc.Instanta.RebindBinding(binding, () => {
            HideApasaPtRebind();
            UpdateVisual();
            });
    }
}
