using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapFarfuriiVisual : MonoBehaviour
{
    [SerializeField] private DulapFarfurii dulap_farfurii;
    [SerializeField] private Transform punctul_de_deasupra;
    [SerializeField] private Transform farfurie_visual;

    private List<GameObject> farfurie_visual_list;

    private void Awake()
    {
        farfurie_visual_list = new List<GameObject>();
    }

    private void Start()
    {
        dulap_farfurii.Cand_Apar_Farfurii += Dulap_Farfurii_Cand_Apar_Farfurii;
        dulap_farfurii.Cand_Dispar_Farfurii += Dulap_Farfurii_Cand_Dispar_Farfurii;

    }
    private void Dulap_Farfurii_Cand_Apar_Farfurii(object sender, System.EventArgs e)
    {
        Transform farfurii_visual_transform = Instantiate(farfurie_visual, punctul_de_deasupra);
        float farfurie_offset = 0.1f;
        
        farfurii_visual_transform.localPosition = new Vector3(0, farfurie_offset* farfurie_visual_list.Count, 0);

        farfurie_visual_list.Add(farfurii_visual_transform.gameObject);
    }
    private void Dulap_Farfurii_Cand_Dispar_Farfurii(object sender, System.EventArgs e)
    {
        GameObject farfurie = farfurie_visual_list[farfurie_visual_list.Count - 1];
        farfurie_visual_list.Remove(farfurie);
        Destroy(farfurie);
    }
}
