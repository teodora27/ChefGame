using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class LivrariManager : MonoBehaviour
{
    public event EventHandler Cand_Reteta_Apare;
    public event EventHandler Cand_Reteta_E_Terminata;
    public event EventHandler Cand_Reteta_E_Buna;
    public event EventHandler Cand_Reteta_Nu_E_Buna;


    public static LivrariManager Instanta {  get; private set; }

    [SerializeField] private ListaReteteSO lista_reteteSO;
    private List<RetetaSO> in_asteptare_reteteSO_lista;
    private float spawn_reteta_timer;
    private float spawn_reteta_timer_max = 4f;
    private int rerete_in_asteptare_max = 4;
    private int livrari_facute;


    private void Awake()
    {
        in_asteptare_reteteSO_lista = new List<RetetaSO> ();
        Instanta = this;
    }

    private void Update()
    {
        spawn_reteta_timer -= Time.deltaTime;
        if (spawn_reteta_timer <= 0f)
        {
            spawn_reteta_timer = spawn_reteta_timer_max;
            if(ManagerJoc.Instance.SeJoaca() && in_asteptare_reteteSO_lista.Count< rerete_in_asteptare_max)
            {
                RetetaSO in_asteptare_reteteSO = lista_reteteSO.reteteSO_lista[UnityEngine.Random.Range(0, lista_reteteSO.reteteSO_lista.Count)];
                //Debug.Log(in_asteptare_reteteSO.nume_reteta);
                in_asteptare_reteteSO_lista.Add(in_asteptare_reteteSO);

                Cand_Reteta_Apare?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }
    public void GataReteta(Farfurie farfurie)
    {
        for(int i=0;i<in_asteptare_reteteSO_lista.Count;i++)
        {
            RetetaSO reteta_in_asteptare = in_asteptare_reteteSO_lista[i];
            if(reteta_in_asteptare.obiecteSO_lista.Count == farfurie.GetMancareSOLista().Count)
            {
                //are acelasi nr de ingrediente
                bool reteta_se_potriveste = true;
                foreach(MancareSO obiect_retetaSO in reteta_in_asteptare.obiecteSO_lista)
                {
                    bool ingredient_gasit = false;
                    //pt ingredientele din reteta
                    foreach (MancareSO obiect_retetaSO_farfurie in farfurie.GetMancareSOLista())
                    {
                        if(obiect_retetaSO_farfurie== obiect_retetaSO)
                        {
                            ingredient_gasit= true;
                            break;
                        }
                    }
                    if(ingredient_gasit==false)
                    {
                        reteta_se_potriveste = false;
                    }

                }
                if( reteta_se_potriveste )
                {
                    //jucatorul a facut comanda corecta
                    //Debug.Log("Bravo");
                    livrari_facute++;

                    in_asteptare_reteteSO_lista.RemoveAt(i);

                    Cand_Reteta_E_Terminata?.Invoke(this, EventArgs.Empty);
                    Cand_Reteta_E_Buna?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
            
        }
        //jucatorul nu a facut comanda buna
        //Debug.Log("mai incerca");
        Cand_Reteta_Nu_E_Buna?.Invoke(this, EventArgs.Empty);

    }
    public List<RetetaSO> GetListaInAsteptare()
    {
        return in_asteptare_reteteSO_lista;

    }
    public int GetLivrariFacute()
    {
        return livrari_facute;
    }
}
