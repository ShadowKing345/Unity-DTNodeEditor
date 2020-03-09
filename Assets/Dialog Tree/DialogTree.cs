using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Tree")]
public class DialogTree : ScriptableObject
{
    public string _id;
    public BasicDialogNode _start_node;
    public BasicDialogNode[] _node_references; 
}
