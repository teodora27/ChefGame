using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livrari_facute_text;

    private void Start()
    {
        ManagerJoc.Instance.Cand_Starea_Se_Schimba += ManagerJoc_Cand_Starea_Se_Schimba;
        Hide();
    }

    private void ManagerJoc_Cand_Starea_Se_Schimba(object sender, System.EventArgs e)
    {
        if (ManagerJoc.Instance.EGameOver())
        {
            Show();
            livrari_facute_text.text = LivrariManager.Instanta.GetLivrariFacute().ToString();

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
