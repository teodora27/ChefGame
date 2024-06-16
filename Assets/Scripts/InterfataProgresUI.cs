using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface InterfataProgresUI 
{
    public event EventHandler<Cand_Progresul_Se_SchimbaEventArgs> Cand_Progresul_Se_Schimba;
    public class Cand_Progresul_Se_SchimbaEventArgs : EventArgs
    {
        public float progres_normalized;
    }

}
