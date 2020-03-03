using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Types/Yes No Dialog")]
public class Dialog_Yes_No : DialogBase
{
    private void Awake()
    {
        base._nexts = new [] {new DialogTether("Yes"), new DialogTether("No") };
    }
}
