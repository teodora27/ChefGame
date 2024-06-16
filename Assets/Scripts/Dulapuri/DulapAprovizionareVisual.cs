using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapAprovizionareVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private DulapAprovizionare dulap_aprovizionare;

    private Animator animatie;

    private void Awake()
    {
        animatie = GetComponent<Animator>();
    }
    private void Start()
    {
        dulap_aprovizionare.Cand_Jucatorul_Ia_Obiectul += Dulap_Aprovizionare_Cand_Jucatorul_Ia_Obiectul;
    }
    private void Dulap_Aprovizionare_Cand_Jucatorul_Ia_Obiectul(object sender, System.EventArgs e)
    {
        animatie.SetTrigger(OPEN_CLOSE);
    }
}
