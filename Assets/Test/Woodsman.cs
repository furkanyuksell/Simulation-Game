using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodsman : VillagerBase
{
    private void Awake()
    {
        selectableTypes.Add(InputController.SelectableTypes.WOODSMAN);
    }
}
