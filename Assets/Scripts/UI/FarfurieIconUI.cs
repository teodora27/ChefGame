using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarfurieIconUI : MonoBehaviour
{
    [SerializeField] private Farfurie farfurie;
    [SerializeField] private Transform icon_tamplate;

    private void Awake()
    {
        icon_tamplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        farfurie.Cand_E_Adaugat_Un_Ingredient += Farfurie_Cand_E_Adaugat_Un_Ingredient;
    }

    private void Farfurie_Cand_E_Adaugat_Un_Ingredient(object sender, Farfurie.Cand_E_Adaugat_Un_IngredientEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == icon_tamplate) continue;
            Destroy(child.gameObject);
        }
        foreach(MancareSO obiectSO in farfurie.GetMancareSOLista())
        {
            Transform icon_transform=Instantiate(icon_tamplate, transform);
            icon_transform.gameObject.SetActive(true);
            icon_transform.GetComponent<FarfurieIconSingurUI>().SetObiectSO(obiectSO);
        }
    }
}
