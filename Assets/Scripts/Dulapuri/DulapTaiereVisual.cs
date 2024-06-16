using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapTaiereVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private DulapTaiere dulap_taiere;

    private Animator animatie;

    private void Awake()
    {
        animatie = GetComponent<Animator>();
    }
    private void Start()
    {
        dulap_taiere.Cand_Taie += DulapTaiere_CandTaie;
    }
    private void DulapTaiere_CandTaie(object sender, System.EventArgs e)
    {
        animatie.SetTrigger(CUT);
    }
}
