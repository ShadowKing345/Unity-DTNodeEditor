using UnityEngine;
using UnityEngine.Events;

///<summary>The class <c>BasicDialogNode</c> is basic Dialog node that is used in the <c>DialogTree</c> class.</summary>
[CreateAssetMenu(menuName = "Dialog/Dialog Types/Simple Dialog")]
public class BasicDialogNode : ScriptableObject
{
    ///<summary>The node before this node.</summary>
    public BasicDialogNode _previous;
    
    ///<summary>A list containing all the tethers to the next dialog nodes.</summary>
    public DialogTether[] _nexts;
    
    ///<summary>The title of the node.</summary>
    public string _title;

    ///<summary>The main text shown in a dialog.</summary>
    [TextArea(3, 10)]
    public string _text; 
    
    ///<summary>A boolean used to determine if the _scriptEvents should be invoked.</summary>
    public bool _shouldFireScriptEvents;
    
    ///<summary>The UnityEvent used to call methods.</summary>
    public UnityEvent _scriptEvents;
}
