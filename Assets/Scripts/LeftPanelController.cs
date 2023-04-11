using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelController : MonoBehaviour
{
    public void OpenCraftMenu()
    {
        transform.position+=new Vector3(110,0,0);
    }
}
