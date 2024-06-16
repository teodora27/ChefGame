using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Farfurie : ObiecteBucatarie
{
    public event EventHandler<Cand_E_Adaugat_Un_IngredientEventArgs> Cand_E_Adaugat_Un_Ingredient;
    public class Cand_E_Adaugat_Un_IngredientEventArgs : EventArgs
    {
        public MancareSO obiectSO;
    }

    [SerializeField] private List<MancareSO> mancare_valida_lista;
    private List<MancareSO> obiecte_lista;
    private void Awake()
    {
        obiecte_lista = new List<MancareSO>();  
    }

    public bool IncearcaAdaugaIngrediect(MancareSO obiectSO)
    {
        if(mancare_valida_lista.Contains(obiectSO))
        {
             if(obiecte_lista.Contains(obiectSO))
             {
                  //are deja ingredientul
                   return false;
             }
            else
            {
                 obiecte_lista.Add(obiectSO);
                Cand_E_Adaugat_Un_Ingredient?.Invoke(this, new Cand_E_Adaugat_Un_IngredientEventArgs
                {
                    obiectSO = obiectSO
                });
                 return true;

            }
        }
        else
        {
            return false;
        }
       
        
    }

    public List<MancareSO> GetMancareSOLista()
    {
        return obiecte_lista;
    }
}
