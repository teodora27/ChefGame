using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivrariManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform reteta_template;

    private void Awake()
    {
        reteta_template.gameObject.SetActive(false);
    }

    private void Start()
    {
        LivrariManager.Instanta.Cand_Reteta_Apare += LivrariManager_Cand_Reteta_Apare;
        LivrariManager.Instanta.Cand_Reteta_E_Terminata += LivrariManager_Cand_Reteta_E_Terminata;
        UpdateVisual();

    }

    private void LivrariManager_Cand_Reteta_E_Terminata(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void LivrariManager_Cand_Reteta_Apare(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == reteta_template) continue;
            Destroy(child.gameObject);
        }
        foreach(RetetaSO retetaSO in LivrariManager.Instanta.GetListaInAsteptare())
        {
            Transform reteta_transform = Instantiate(reteta_template, container);
            reteta_transform.gameObject.SetActive(true);
            reteta_transform.GetComponent<LivrariManagerRetetaUI>().SetRetetaSO(retetaSO);
        }

    }
}
