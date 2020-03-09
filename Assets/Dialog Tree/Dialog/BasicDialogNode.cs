using UnityEngine;
using UnityEngine.Events;

/**
 * The basic dialog node class.
 */
[CreateAssetMenu(menuName = "Dialog/Dialog Types/Simple Dialog")]
public class BasicDialogNode : ScriptableObject
{
    //The node before this node.
    public BasicDialogNode _previous;
    //A list containing all the tethers to the next dialog nodes.
    public DialogTether[] _nexts;
    
    //The title of the node.
    public string _title;

    [TextArea(3, 10)]
    //The main text shown in a dialog.
    public string _text;
    //A boolean used to determine if the _scriptEvents should be invoked.
    public bool _shouldFireScriptEvents;
    //The UnityEvent used to call methods.
    public UnityEvent _scriptEvents;
}
