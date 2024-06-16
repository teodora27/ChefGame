using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObiecteBucatarie : MonoBehaviour
{
    [SerializeField] private MancareSO obiectSO;

    private InterfataObiectParent obiect_parinte;

    public MancareSO GetMancareSO() 
    { 
        return obiectSO; 
    }

    public void SetObiectParinte(InterfataObiectParent obiect_parinte)
    {
        if(this.obiect_parinte != null)
        {
            this.obiect_parinte.ClearObiect(); //sterge obiectul de pe locul vechi
        }

        this.obiect_parinte = obiect_parinte;

        if(obiect_parinte.AreObiect())
        {
            Debug.LogError("Nu merge, are deja obiect");
        }
        obiect_parinte.SetObiect(this); //pune obiectul rin locul nou

        transform.parent = obiect_parinte.ObiectSeMuta();
        transform.localPosition = Vector3.zero;
    }
    public InterfataObiectParent GetObiectParinte()
    {
        return obiect_parinte;
    }

    public void Autodistrugere()
    {
        obiect_parinte.ClearObiect();
        Destroy(gameObject);
    }

    public bool IncearcaSaPuiInFarfurie(out Farfurie farfurie)
    {
        if(this is Farfurie)
        {
            farfurie = this as Farfurie;
            return true;
        }
        else
        {
            farfurie = null;
            return false;
        }
    }

    public static ObiecteBucatarie SpawnObiect(MancareSO obiectSO, InterfataObiectParent obiect_parinte)
    {
        Transform obiect_taiat_transformat = Instantiate(obiectSO.prefab); //apare obiectul
        ObiecteBucatarie obiect = obiect_taiat_transformat.GetComponent<ObiecteBucatarie>();
        obiect.SetObiectParinte(obiect_parinte);

        return obiect;

    }

}
