using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialog_Manager : MonoBehaviour
{
    private Dialog_Tree _tree;
    private Dialog_Base _current_dialog;
    public UnityEvent OnDialogTreeBegin;
    public UnityEvent OnDialogTreeEnd;
    public UnityEvent OnDialogBegin;
    public UnityEvent OnDialogEnd;
    public UnityEvent onBranch;
    public UnityEvent onPathChosen;
    public UnityEvent onProgress;

    public void StartDialog(Dialog_Tree Tree)
    {
        _tree = Tree;
        //Todo: Make the events pass the variables.
        OnDialogTreeBegin.Invoke();
        _current_dialog = _tree._start_node;
        OnDialogBegin.Invoke();
    }

    public void Progress(Dialog_Base Location)
    {
        if(_current_dialog == null)
            throw new Exception("Cannot find current dialog.");
        if (Location == null)
        {
            TerminateDialog();
        }
        onProgress.Invoke();
    }

    public void TerminateDialog()
    {
        _tree = null;
        _current_dialog = null;
        OnDialogTreeEnd.Invoke();
    }
}
