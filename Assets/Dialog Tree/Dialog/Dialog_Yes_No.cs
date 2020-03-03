using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Types/Yes No Dialog")]
public class Dialog_Yes_No : Dialog_Base
{
    private void Awake()
    {
        base._nexts = new [] {new Dialog_Tether("Yes"), new Dialog_Tether("No") };
    }
}
