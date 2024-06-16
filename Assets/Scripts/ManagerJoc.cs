using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerJoc : MonoBehaviour
{
    public static ManagerJoc Instance {  get; private set; }

    public event EventHandler Cand_Starea_Se_Schimba;
    public event EventHandler Cand_Pune_Pauza;
    public event EventHandler Cand_Iese_Din_Pauza;

    private enum Stare
    {
        AsteptareSaInceapa,
        NumaratoareaInversa,
        SeJoaca,
        GameOver,
    }
    private Stare stare;
    //private float asteptare_sa_inceapa_timer=1f;
    private float numaratoarea_inversa_timer = 3f;
    private float joaca_timer;
    private float joaca_timer_max = 100f;
    private bool e_pauza = false;


    private void Awake()
    {
        stare = Stare.AsteptareSaInceapa;
        Instance = this;
    }

    private void Start()
    {
        InputJoc.Instanta.Cand_Pune_Pauza += InputJoc_Cand_Pune_Pauza;
        InputJoc.Instanta.Cand_Interactioneaza += InputJoc_Cand_Interactioneaza;
    }

    private void InputJoc_Cand_Interactioneaza(object sender, EventArgs e)
    {
        if(stare == Stare.AsteptareSaInceapa)
        {
            stare = Stare.NumaratoareaInversa;
            Cand_Starea_Se_Schimba?.Invoke(this, EventArgs.Empty);
        }
    }

    private void InputJoc_Cand_Pune_Pauza(object sender, EventArgs e)
    {
        Pauza();
    }

    private void Update()
    {
        switch (stare)
        {
            case Stare.AsteptareSaInceapa:
                /*asteptare_sa_inceapa_timer -= Time.deltaTime;
                if(asteptare_sa_inceapa_timer < 0 )
                {
                    stare = Stare.NumaratoareaInversa;
                    Cand_Starea_Se_Schimba?.Invoke(this, EventArgs.Empty);
                }*/
                break;
            case Stare.NumaratoareaInversa:
                numaratoarea_inversa_timer -= Time.deltaTime;
                if (numaratoarea_inversa_timer < 0)
                {
                    stare = Stare.SeJoaca;
                    joaca_timer = joaca_timer_max;
                    Cand_Starea_Se_Schimba?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Stare.SeJoaca:
                joaca_timer -= Time.deltaTime;
                if (joaca_timer < 0)
                {
                    stare = Stare.GameOver;
                    Cand_Starea_Se_Schimba?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Stare.GameOver:
                break;
        }
        //Debug.Log(stare);
    }

    public bool SeJoaca()
    {
        return stare == Stare.SeJoaca;
    }

    public bool MergeNumaratoareaInversa()
    {
        return stare ==Stare.NumaratoareaInversa;
    }
    public float GetNumaratoareInversaTimp()
    {
        return numaratoarea_inversa_timer;
    }
    public bool EGameOver()
    {
        return stare == Stare.GameOver;
    }

    public float GetTimerJoc()
    {
        return 1- joaca_timer / joaca_timer_max;
    }

    public void Pauza()
    {
        e_pauza = !e_pauza;
        if(e_pauza)
        {
            Time.timeScale = 0f;
            Cand_Pune_Pauza?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            Cand_Iese_Din_Pauza.Invoke(this, EventArgs.Empty);
        }
    }
}
