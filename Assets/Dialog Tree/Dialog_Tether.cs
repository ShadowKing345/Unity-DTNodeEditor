using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog_Tether
{
    public string _text;
    public Dialog_Base _destination;

    public Dialog_Tether(string text)
    {
        _text = text;
    }
    
    public Dialog_Tether(string text, Dialog_Base destination)
    {
        _text = text;
        _destination = destination;
    }

    public Dialog_Tether(Dialog_Base destination)
    {
        _text = "";
        _destination = destination;
    }
}
