using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBarUI : MonoBehaviour
{
    [SerializeField] private Aragaz aragaz;
    private Animator animator;
    private const string FLASHING = "flashing";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        aragaz.Cand_Progresul_Se_Schimba += Aragaz_Cand_Progresul_Se_Schimba;
        animator.SetBool(FLASHING, false);

    }

    private void Aragaz_Cand_Progresul_Se_Schimba(object sender, InterfataProgresUI.Cand_Progresul_Se_SchimbaEventArgs e)
    {
        float ardere_progres = .5f;
        bool show = aragaz.Se_Arde() && e.progres_normalized >= ardere_progres;
        animator.SetBool(FLASHING, show);
    }
   
}
