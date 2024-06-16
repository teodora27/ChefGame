using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputJoc : MonoBehaviour
{

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static InputJoc Instanta { get; private set; }

    public event EventHandler Cand_Interactioneaza;//definitie event OnInteractAction
    public event EventHandler Cand_Interactioneaza_Alternativ;
    public event EventHandler Cand_Pune_Pauza;
    public event EventHandler Cand_Rebind;

    public enum Binding
    {
        Sus,
        Jos,
        Stanga,
        Dreapta,
        Interactiune,
        InteractiuneAlternativa,
        Pauza
    }

    private InputJucator input_jucator;

    private void Awake()
    {
        Instanta = this;
        input_jucator = new InputJucator();

        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            input_jucator.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        input_jucator.Jucator.Enable();
      
        input_jucator.Jucator.Interactiune.performed += Interactiune_performed;  //subscribe to the performed event
        input_jucator.Jucator.InteractiuneAlternativa.performed += InteractiuneAlternativa_performed;
        input_jucator.Jucator.Pauza.performed += Pauza_performed;

        //Debug.Log(GetBindingText(Binding.Interactiune));
        

    }

    private void OnDestroy() //cand se schimba scena se schimba, obiectul de care e atasat InputJoc e distrus, dar inputurile pt jucator si evenimentele nu asa ca mai jos se da unsubscibe manual
    {
        input_jucator.Jucator.Interactiune.performed -= Interactiune_performed;  //subscribe to the performed event
        input_jucator.Jucator.InteractiuneAlternativa.performed -= InteractiuneAlternativa_performed;
        input_jucator.Jucator.Pauza.performed -= Pauza_performed;

        input_jucator.Dispose();
    }

    private void Pauza_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Cand_Pune_Pauza?.Invoke(this, EventArgs.Empty);
    }

    private void InteractiuneAlternativa_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Cand_Interactioneaza_Alternativ?.Invoke(this, EventArgs.Empty);
    }

    private void Interactiune_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //Debug.Log("interactiune");
        Cand_Interactioneaza?.Invoke(this, EventArgs.Empty);   //trigger event
    }

   public Vector2 Miscare()
    {
        Vector2 directie = input_jucator.Jucator.Miscare.ReadValue<Vector2>();
        

       /* if (Input.GetKey(KeyCode.W))
        {
            directie.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            directie.y = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            directie.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            directie.x = +1;
        }
        */
        directie = directie.normalized;
       
        return directie;
    }

    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Sus:
                return input_jucator.Jucator.Miscare.bindings[1].ToDisplayString();
            case Binding.Jos:
                return input_jucator.Jucator.Miscare.bindings[2].ToDisplayString();
            case Binding.Stanga:
                return input_jucator.Jucator.Miscare.bindings[3].ToDisplayString();
            case Binding.Dreapta:
                return input_jucator.Jucator.Miscare.bindings[4].ToDisplayString();


            case Binding.Interactiune:
                return input_jucator.Jucator.Interactiune.bindings[0].ToDisplayString();
            case Binding.InteractiuneAlternativa:
                return input_jucator.Jucator.InteractiuneAlternativa.bindings[0].ToDisplayString();
            case Binding.Pauza:
                return input_jucator.Jucator.Pauza.bindings[0].ToDisplayString();

        }
    }
    public void RebindBinding(Binding binding, Action cand_rebind)
    {
        input_jucator.Jucator.Disable();

        InputAction input_actiune;
        int binding_numar;

        switch (binding)
        {
            default:
            case Binding.Sus:
                input_actiune = input_jucator.Jucator.Miscare;
                binding_numar = 1;
                break;
            case Binding.Jos:
                input_actiune = input_jucator.Jucator.Miscare;
                binding_numar = 2;
                break;
            case Binding.Stanga:
                input_actiune = input_jucator.Jucator.Miscare;
                binding_numar = 3;
                break;
            case Binding.Dreapta:
                input_actiune = input_jucator.Jucator.Miscare;
                binding_numar = 4;
                break;
            case Binding.Interactiune:
                input_actiune = input_jucator.Jucator.Interactiune;
                binding_numar = 0;
                break;
            case Binding.InteractiuneAlternativa:
                input_actiune = input_jucator.Jucator.InteractiuneAlternativa;
                binding_numar = 0;
                break;
            case Binding.Pauza:
                input_actiune = input_jucator.Jucator.Pauza;
                binding_numar = 0;
                break;
        }

        input_actiune.PerformInteractiveRebinding(binding_numar).OnComplete(callback =>
        {
            //Debug.Log(callback.action.bindings[1].path);
            callback.Dispose();
            input_jucator.Jucator.Enable();
            cand_rebind();

            //input_jucator.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, input_jucator.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            Cand_Rebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }
}
