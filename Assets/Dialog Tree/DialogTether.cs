using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogTether
{
    public string _text;
    public DialogBase _destination;

    public DialogTether(string text)
    {
        _text = text;
    }
    
    public DialogTether(string text, DialogBase destination)
    {
        _text = text;
        _destination = destination;
    }

    public DialogTether(DialogBase destination)
    {
        _text = "";
        _destination = destination;
    }
}
