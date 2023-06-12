using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EmptyParentObject : NetworkBehaviour
{
    [ClientRpc]
    private void SetNameOnClientRpc(string parentName)
    {
        name = parentName;
    }

    public void SetNewName(string parentName)
    {
        name = parentName;
        SetNameOnClientRpc(parentName);
    }
}
