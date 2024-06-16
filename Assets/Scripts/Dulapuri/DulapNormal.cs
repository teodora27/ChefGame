using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DulapNormal : BazaDulap
{
    [SerializeField] private MancareSO obiect_bucatarie;

    public override void Interactiune(Jucator jucator) //Interact
    {
        if(!AreObiect())//nu e niciun obiect pe dulap
        {
            if(jucator.AreObiect())
            {
                jucator.GetObiect().SetObiectParinte(this);
            }
            else
            {

            }
        }
        else // e ceva pe dulap
        {
            if(jucator.AreObiect())
            {
                if(jucator.GetObiect().IncearcaSaPuiInFarfurie(out Farfurie farfurie)) // jucatorul duce o farfurie
                {
                    //jucatorul duce o farfurie
                    if(farfurie.IncearcaAdaugaIngrediect(GetObiect().GetMancareSO()))
                    {
                         GetObiect().Autodistrugere();

                    }
                }
                else
                {
                    // jucatorul are un obiect care nu e farfurie
                    if(GetObiect().IncearcaSaPuiInFarfurie(out farfurie))
                    {
                        //pe dulap e o farfurie
                       if( farfurie.IncearcaAdaugaIngrediect(jucator.GetObiect().GetMancareSO()))
                       {
                            jucator.GetObiect().Autodistrugere();
                       }
                    }
                }
            }
            else
            {
                GetObiect().SetObiectParinte(jucator);
            }
        }
    }

    
}
