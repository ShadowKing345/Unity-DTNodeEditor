using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Types/Okay Dialog")]
public class Dialog_Okay : Dialog_Base
{
    public Dialog_Okay() : base()
    {
        _nexts = new[] {new Dialog_Tether("Okay")};
    }

    public Dialog_Okay(string okayMessage, Dialog_Base okayDestination) : base()
    {
        _nexts = new[] {new Dialog_Tether(okayMessage, okayDestination)};
    }
}
