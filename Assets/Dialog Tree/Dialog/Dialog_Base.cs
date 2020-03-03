using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialog/Dialog Types/Simple Dialog")]
public class Dialog_Base : ScriptableObject
{
    public Dialog_Base _previous;
    public Dialog_Tether[] _nexts;
    
    public string _id;
    public string _title;

    public string _text;
    public bool _shouldFireScriptEvents;
    public UnityEvent _scriptEvents;
}
