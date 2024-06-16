using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jucator : MonoBehaviour, InterfataObiectParent
{
    //public static Jucator instanta;
    //static inseamna ca apartine clasei, nu jucatorului individual
    
    public static Jucator Instanta { get; private set; }

    [SerializeField] private float viteza = 7f;
    [SerializeField] private InputJoc input;
    [SerializeField] private LayerMask dulap_layer_mask;
    [SerializeField] private Transform punctul_de_prindere;

    public event EventHandler <Cand_Dulapul_E_SelectatEventArgs> Cand_Dulapul_E_Selectat;
    public class Cand_Dulapul_E_SelectatEventArgs : EventArgs
    {
        public BazaDulap dulap_selectat;
    }

    public event EventHandler Cand_Apuca_Ceva;

    private bool merge;
    private Vector3 ultima_interactiune;
    private BazaDulap dulap_selectat;
    private ObiecteBucatarie obiect;

    private void Awake()
    {
        if(Instanta != null)
        {
            Debug.LogError("Exista mai mult de un jucator");
        }
        Instanta = this;
    }

    private void Start()
    {
        input.Cand_Interactioneaza += Input_Cand_Interactioneaza; //subscribe to the event
        input.Cand_Interactioneaza_Alternativ += Input_Cand_Interactioneaza_Alternativ;
    }

    private void Input_Cand_Interactioneaza(object sender, System.EventArgs e) //GameInput_OnInterationsAction
    {
        if (!ManagerJoc.Instance.SeJoaca()) return;
        if (dulap_selectat != null)
        {
            dulap_selectat.Interactiune(this);
        }
    }

    private void Input_Cand_Interactioneaza_Alternativ(object sender, System.EventArgs e)
    {
        if (!ManagerJoc.Instance.SeJoaca()) return;

        if (dulap_selectat != null)
        {
            dulap_selectat.InteractiuneAlternativa(this);
        }
    }

    private void Update()
    {
        MersJucator();
        Interactiuni();
    }

    private void MersJucator()
    {
        Vector2 directie = input.Miscare();
        Vector3 miscare = new Vector3(directie.x, 0f, directie.y);
        float raza_jucator = 0.68f;
        float inaltime_jucator = 2f;
        float distanta = Time.deltaTime * viteza;
        bool poate_sa_mearga = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * inaltime_jucator, raza_jucator, miscare, distanta);
        if (!poate_sa_mearga)
        {
            //ca sa poata sa mearga exact pe langa un obiect
            //daca nu poate merge se verifica daca se poate misca pe alte directii
            Vector3 miscareX = new Vector3(miscare.x, 0, 0).normalized;
            poate_sa_mearga =miscare.x!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * inaltime_jucator, raza_jucator, miscareX, distanta);
            if (poate_sa_mearga)
            {
                miscare = miscareX;
            }
            else
            {
                Vector3 miscareZ = new Vector3(0, 0, miscare.z).normalized;
                poate_sa_mearga =miscare.z!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * inaltime_jucator, raza_jucator, miscareZ, distanta);
                if (poate_sa_mearga)
                {
                    miscare = miscareZ;
                }
            }

        }
        if (poate_sa_mearga)
        {
            transform.position += miscare * Time.deltaTime * viteza;
        }


        merge = (miscare != Vector3.zero);
        
        float viteza_rotatie = 10f;
        transform.forward = Vector3.Slerp(transform.forward, miscare, Time.deltaTime * viteza_rotatie);
       

    }

    public bool Merge()
    {
        return merge;
    }

    private void Interactiuni() //HandleInteractions
    {
        Vector2 directie = input.Miscare();
        Vector3 miscare = new Vector3(directie.x, 0f, directie.y);
        
        if(miscare != Vector3.zero)
        {
            ultima_interactiune = miscare;
        }

        float distanta_pentru_interactiuni = 2f;
        if(Physics.Raycast(transform.position, ultima_interactiune, out RaycastHit raycasthit, distanta_pentru_interactiuni, dulap_layer_mask))
        {
            // Debug.Log(raycasthit.transform); //ce loveste
            //DulapNormal dulap = raycasthit.transform.GetComponent<DulapNormal>();
            if(raycasthit.transform.TryGetComponent(out BazaDulap dulap))
            {
                //daca are componenta ClearCounter
                //dulap.Interactiune(this); 
                if (dulap != dulap_selectat)
                {
                    SeteazaSelectareaDulapului(dulap);
                }
                else
                {
                    //SeteazaSelectareaDulapului(null);
                }     
            }
            else
            {
                SeteazaSelectareaDulapului(null);
            }

        }
        else
        {
            SeteazaSelectareaDulapului(null);
        }

    }

    private void SeteazaSelectareaDulapului(BazaDulap dulap_selectat)
    {
        this.dulap_selectat = dulap_selectat;
        Cand_Dulapul_E_Selectat?.Invoke(this, new Cand_Dulapul_E_SelectatEventArgs
        {
            dulap_selectat = dulap_selectat
        });
    }

    public Transform ObiectSeMuta()
    {
        return punctul_de_prindere;
    }
    public void SetObiect(ObiecteBucatarie obiect)
    {
        this.obiect = obiect;
        if(obiect!=null)
        {
            Cand_Apuca_Ceva?.Invoke(this, EventArgs.Empty);
        }
    }
    public ObiecteBucatarie GetObiect()
    {
        return obiect;
    }
    public void ClearObiect()
    {
        obiect = null;
    }
    public bool AreObiect()
    {
        return obiect != null;
    }

}
