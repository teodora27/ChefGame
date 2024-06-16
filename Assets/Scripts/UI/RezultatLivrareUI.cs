using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RezultatLivrareUI : MonoBehaviour
{
    private const string POPUP = "PopUp";

    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI mesaj;
    [SerializeField] private Color culoare_succes;
    [SerializeField] private Color culoare_fail;
    [SerializeField] private Sprite sprite_succes;
    [SerializeField] private Sprite sprite_fail;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        LivrariManager.Instanta.Cand_Reteta_E_Buna += LivrariManager_Cand_Reteta_E_Buna;
        LivrariManager.Instanta.Cand_Reteta_Nu_E_Buna += LivrariManager_Cand_Reteta_Nu_E_Buna;
        gameObject.SetActive(false);
    }

    private void LivrariManager_Cand_Reteta_Nu_E_Buna(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        background.color = culoare_fail;
        icon.sprite = sprite_fail;
        mesaj.text = "NASPA";
    }

    private void LivrariManager_Cand_Reteta_E_Buna(object sender, System.EventArgs e)
    {
        animator.SetTrigger(POPUP);
        gameObject.SetActive(true);
        background.color = culoare_succes;
        icon.sprite = sprite_succes;
        mesaj.text = "BRAVO";

    }
}
