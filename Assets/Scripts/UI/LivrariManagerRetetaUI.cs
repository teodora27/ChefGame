using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivrariManagerRetetaUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nume_reteta;
    [SerializeField] private Transform icon_container;
    [SerializeField] private Transform icon_tamplate;

    private void Awake()
    {
        icon_tamplate.gameObject.SetActive(false);
    }

    public void SetRetetaSO(RetetaSO retetaSO)
    {
        nume_reteta.text = retetaSO.nume_reteta;
        foreach(Transform child in icon_container)
        {
            if (child == icon_tamplate) continue;
            Destroy(child.gameObject);
        }
        foreach(MancareSO obiectSO in retetaSO.obiecteSO_lista)
        {
            Transform icon_transform =Instantiate(icon_tamplate, icon_container);
            icon_transform.gameObject.SetActive(true);
            icon_transform.GetComponent<Image>().sprite = obiectSO.sprite;
        }
    }
}
