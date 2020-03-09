using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialog/Dialog Types/Simple Dialog")]
public class DialogBase : ScriptableObject
{
    public DialogBase _previous;
    public DialogTether[] _nexts;
    
    public string _id;
    public string _title;

    [TextArea(3, 10)]
    public string _text;
    public bool _shouldFireScriptEvents;
    public UnityEvent _scriptEvents;
}
