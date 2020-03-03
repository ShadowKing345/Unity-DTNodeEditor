using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog Tree")]
public class Dialog_Tree : ScriptableObject
{
    public string _id;
    public Dialog_Base _start_node;
    public Dialog_Base[] _node_references; 
}
