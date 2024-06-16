using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatie : MonoBehaviour
{
    private Animator animatie;
    [SerializeField] private Jucator jucator;
    private void Awake()
    {
        animatie = GetComponent<Animator>();
    }
    private void Update()
    {
        animatie.SetBool("merge", jucator.Merge());
    }
}
