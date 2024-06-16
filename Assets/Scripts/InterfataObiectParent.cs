using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfataObiectParent
{
    public Transform ObiectSeMuta();

    public void SetObiect(ObiecteBucatarie obiect);

    public ObiecteBucatarie GetObiect();

    public void ClearObiect();

    public bool AreObiect();
    
}
