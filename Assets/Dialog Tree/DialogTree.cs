using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Tree")]
public class DialogTree : ScriptableObject
{
    public string _id;
    public DialogBase _start_node;
    public DialogBase[] _node_references; 
}
