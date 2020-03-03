using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Types/Okay Dialog")]
public class Dialog_Okay : DialogBase
{
    public Dialog_Okay() : base()
    {
        _nexts = new[] {new DialogTether("Okay")};
    }

    public Dialog_Okay(string okayMessage, DialogBase okayDestination) : base()
    {
        _nexts = new[] {new DialogTether(okayMessage, okayDestination)};
    }
}
