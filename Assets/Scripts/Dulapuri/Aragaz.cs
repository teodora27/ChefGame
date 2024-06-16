using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Aragaz : BazaDulap, InterfataProgresUI
{
    public event EventHandler<InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs> Cand_Progresul_Se_Schimba;

    public event EventHandler<Cand_Starea_Se_SchimbaEventArgs> Cand_Starea_Se_Schimba;
    public class Cand_Starea_Se_SchimbaEventArgs : EventArgs
    {
        public Stare stare;
    }

    public enum Stare
    {
        Nimic,
        Gatire,
        Gatit,
        Ars,
    }

    private Stare stare;

    [SerializeField] private GatireRetetaSO[] gatireSO_vector;
    [SerializeField] private ArdereRetetaSO[] ardereSO_vector;

    private float timp_gatire, timp_ardere;
    private GatireRetetaSO gatireSO;
    private ArdereRetetaSO ardereSO;

    private void Start()
    {
        stare = Stare.Nimic;
    }

    private void Update()
    {
        if (AreObiect())
        {
            switch (stare)
             {
                 case Stare.Nimic:
                      break;
                 case Stare.Gatire:
                       timp_gatire += Time.deltaTime;
                    Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                    {
                        progres_normalized = timp_gatire / gatireSO.gatire_timp_maxim
                    });
                    if (timp_gatire > gatireSO.gatire_timp_maxim)
                         {
                            //s a prajit
                          
                            GetObiect().Autodistrugere();
                            ObiecteBucatarie.SpawnObiect(gatireSO.output, this);
                        stare = Stare.Gatit;
                        timp_ardere = 0f;                        
                        ardereSO = Get_Reteta_Ardere(GetObiect().GetMancareSO());


                        Cand_Starea_Se_Schimba?.Invoke(this, new Cand_Starea_Se_SchimbaEventArgs
                        {
                            stare = stare
                        }) ;
                        //Debug.Log("Prajit");
                         }
                      break;
                 case Stare.Gatit:
                    timp_ardere += Time.deltaTime;
                    Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                    {
                        progres_normalized = timp_ardere / ardereSO.ardere_timp_maxim
                    });
                    if (timp_ardere > ardereSO.ardere_timp_maxim)
                    {
                        //s a ars

                        GetObiect().Autodistrugere();
                        ObiecteBucatarie.SpawnObiect(ardereSO.output, this);
                        stare = Stare.Ars;

                        Cand_Starea_Se_Schimba?.Invoke(this, new Cand_Starea_Se_SchimbaEventArgs
                        {
                            stare = stare
                        });
                        Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                        {
                            progres_normalized = 0f
                        });
                        //Debug.Log("Ars");
                    }
                    break;
                 case Stare.Ars:
                      break;

             }
         
        }

       
    }

    IEnumerator Gatire(float time)
    {
        yield return new WaitForSeconds(time);

        //s a prajit

        GetObiect().Autodistrugere();
        ObiecteBucatarie.SpawnObiect(gatireSO.output, this);
        stare = Stare.Gatit;
        timp_ardere = 0f;
        ardereSO = Get_Reteta_Ardere(GetObiect().GetMancareSO());


        Cand_Starea_Se_Schimba?.Invoke(this, new Cand_Starea_Se_SchimbaEventArgs
        {
            stare = stare
        });
        //Debug.Log("Prajit");
    }

    public override void Interactiune(Jucator jucator)
    {
        if (!AreObiect())//nu e niciun obiect pe dulap
        {
            if (jucator.AreObiect())
            {
                if (AreRetetaCuInput(jucator.GetObiect().GetMancareSO()))
                {
                    jucator.GetObiect().SetObiectParinte(this);
                    gatireSO = Get_Reteta_Gatire(GetObiect().GetMancareSO());
                    stare = Stare.Gatire;
                    //StartCoroutine(Gatire(4f));
                    timp_gatire = 0f;

                    Cand_Starea_Se_Schimba?.Invoke(this, new Cand_Starea_Se_SchimbaEventArgs
                    {
                        stare = stare
                    });

                    Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                    {
                        progres_normalized = timp_gatire / gatireSO.gatire_timp_maxim
                    }) ;
                }

            }
            else
            {

            }
        }
        
        else // e ceva pe dulap
        {
            if (jucator.AreObiect())
            {
                if (jucator.GetObiect().IncearcaSaPuiInFarfurie(out Farfurie farfurie)) // jucatorul duce o farfurie
                {
                    //jucatorul duce o farfurie

                    if (farfurie.IncearcaAdaugaIngrediect(GetObiect().GetMancareSO()))
                    {
                        GetObiect().Autodistrugere();

                        stare = Stare.Nimic;

                        Cand_Starea_Se_Schimba?.Invoke(this, new Cand_Starea_Se_SchimbaEventArgs
                        {
                            stare = stare
                        });

                        Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                        {
                            progres_normalized = 0f
                        });
                    }
                }
            }
            else
            {
                GetObiect().SetObiectParinte(jucator);
                stare = Stare.Nimic;

                Cand_Starea_Se_Schimba?.Invoke(this, new Cand_Starea_Se_SchimbaEventArgs
                {
                    stare = stare
                });

                Cand_Progresul_Se_Schimba?.Invoke(this, new InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs
                {
                    progres_normalized = 0f
                });
            }
        }
    }

    private bool AreRetetaCuInput(MancareSO inputSO)
    {
        GatireRetetaSO gatireSO = Get_Reteta_Gatire(inputSO);
        return gatireSO != null;

    }

    private MancareSO GetOutputFromInput(MancareSO input_mancareSO) //GetOutputFromInput
    {
        GatireRetetaSO gatireSO = Get_Reteta_Gatire(input_mancareSO);
        if (gatireSO != null)
        {
            return gatireSO.output;
        }
        else
        {
            return null;
        }

    }
    private GatireRetetaSO Get_Reteta_Gatire(MancareSO input_mancareSO) //GetCuttingRecipeSOWithInput
    {
        foreach (GatireRetetaSO gatireSO in gatireSO_vector)
        {
            if (gatireSO.input == input_mancareSO)
            {
                return gatireSO;
            }
        }
        return null;
    }
    private ArdereRetetaSO Get_Reteta_Ardere(MancareSO input_mancareSO) //GetCuttingRecipeSOWithInput
    {
        foreach (ArdereRetetaSO ardereSO in ardereSO_vector)
        {
            if (ardereSO.input == input_mancareSO)
            {
                return ardereSO;
            }
        }
        return null;
    }
    public bool Se_Arde()
    {
        return stare == Stare.Gatit;
    }
}
