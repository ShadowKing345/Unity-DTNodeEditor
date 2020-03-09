using UnityEngine;

/**
 * This is a main tree for the dialog.
 */
[CreateAssetMenu(menuName = "Dialog/Dialog Tree")]
public class DialogTree : ScriptableObject
{
    //The Starting node.
    public BasicDialogNode _startNode;
    //This is a list of all the dialog nodes contained in the tree object.
    public BasicDialogNode[] _nodeReferences; 
}
