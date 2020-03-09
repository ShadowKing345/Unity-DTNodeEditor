using UnityEngine;

/// <summary>The main container for all the Dialog Nodes.</summary>
[CreateAssetMenu(menuName = "Dialog/Dialog Tree")]
public class DialogTree : ScriptableObject
{
    ///<summary>The starting dialog node.</summary>
    public BasicDialogNode _startNode;
    ///<summary>This is a list of all the dialog nodes contained in the tree object.</summary>
    public BasicDialogNode[] _nodeReferences; 
}
