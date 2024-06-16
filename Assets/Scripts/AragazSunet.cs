using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AragazSunet : MonoBehaviour
{
    [SerializeField] private Aragaz aragaz;
    private AudioSource sursa_audio;
    private float semnal_ardere_sunet_timer;
    private bool play_semnal_ardere;

    private void Awake()
    {
        sursa_audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        aragaz.Cand_Starea_Se_Schimba += Aragaz_Cand_Starea_Se_Schimba;
        aragaz.Cand_Progresul_Se_Schimba += Aragaz_Cand_Progresul_Se_Schimba;
    }

    private void Aragaz_Cand_Progresul_Se_Schimba(object sender, InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs e)
    {
        float ardere_progres = .5f;
        play_semnal_ardere = aragaz.Se_Arde() && e.progres_normalized >= ardere_progres;



    }

    private void Aragaz_Cand_Starea_Se_Schimba(object sender, Aragaz.Cand_Starea_Se_SchimbaEventArgs e)
    {
        bool play = e.stare == Aragaz.Stare.Gatire || e.stare == Aragaz.Stare.Gatit; 
        if (play)
        {
            sursa_audio.Play();
        }
        else { sursa_audio.Pause();}

        
    }
    private void Update()
    {
        if(play_semnal_ardere)
        {
            semnal_ardere_sunet_timer -= Time.deltaTime;
             if(semnal_ardere_sunet_timer<=0f)
             {
                  float semnal_ardere_timer_max = .2f;
                 semnal_ardere_sunet_timer = semnal_ardere_timer_max;

                SunetManager.Instance.PlaySemnalArdere(aragaz.transform.position);
             }
        }
        
    }
}
