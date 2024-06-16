using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarfuriePlinaVisual : MonoBehaviour
{
    [Serializable]
    public struct ObiectSO_Gameobject
    {
        public MancareSO obiectSO;
        public GameObject gameObject;
    }

    [SerializeField] private Farfurie farfurie;
    [SerializeField] private List<ObiectSO_Gameobject> obiectSO_gameobject_lista;

    private void Start()
    {
        farfurie.Cand_E_Adaugat_Un_Ingredient += Farfurie_Cand_E_Adaugat_Un_Ingredient;
        foreach (ObiectSO_Gameobject obiectSO_gameobject in obiectSO_gameobject_lista)
        {
              obiectSO_gameobject.gameObject.SetActive(false);
        }
    }

    private void Farfurie_Cand_E_Adaugat_Un_Ingredient(object sender, Farfurie.Cand_E_Adaugat_Un_IngredientEventArgs e)
    {
        foreach(ObiectSO_Gameobject obiectSO_gameobject in obiectSO_gameobject_lista)
        {
            if(obiectSO_gameobject.obiectSO == e.obiectSO)
            {
                obiectSO_gameobject.gameObject.SetActive(true);
            }
        }
       
    }
}
