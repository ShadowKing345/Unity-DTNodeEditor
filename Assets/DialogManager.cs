using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public void Awake()
    {
        instance = this;
    }

    private DialogTree _tree;
    private DialogBase _currentDialog;
    
    [Serializable]
    public class DialogTreeEvent : UnityEvent<DialogTree> {}
    public DialogTreeEvent onDialogBegin;
    public UnityEvent onDialogEnd;

    [Serializable]
    public class DialogEvent : UnityEvent<DialogBase> {}
    public DialogEvent onDialogBoxBegin;
    public UnityEvent onDialogBoxEnd;
    
    [Serializable]
    public class BranchDetectedClass : UnityEvent<DialogTether[]> {}
    public BranchDetectedClass onBranchDetected;
    
    [Serializable]
    public class DialogTetherEvent : UnityEvent<DialogTether> {}
    public DialogTetherEvent onPathChosen;
    public DialogTetherEvent onProgress;

    public UnityEvent onDialogBoxScriptFire;
    
    public void StartDialog(DialogTree tree)
    {
        if (_tree)
        {
            Debug.LogError("A dialog has already started. You cannot start a new one.");
            return;
        }
        
        _tree = tree;
        onDialogBegin.Invoke(tree);
     
        SetDialogBox(tree._start_node);
    }

    public void SetDialogBox(DialogBase dialog)
    {
        if (dialog == null)
        {
            TerminateDialog(); 
            return;
        }

        _currentDialog = dialog;
        onDialogBoxBegin.Invoke(dialog);

        CheckIfBranching(dialog._nexts);
    }

    public void CheckIfBranching(DialogTether[] tethers)
    {
        if (tethers.Length > 1)
        {
            onBranchDetected.Invoke(tethers);
        }
        else
        {
            if(!string.IsNullOrEmpty(tethers[0]._text) && !string.IsNullOrWhiteSpace(tethers[0]._text)) onBranchDetected.Invoke(tethers);
        }
    }
    
    public void BranchChosen()
    {
        DialogTether tether = _currentDialog._nexts[0];

        if (tether != null && tether._destination == null)
        {
            onPathChosen.Invoke(tether);
            TerminateDialog();
            return;
        }
        onPathChosen.Invoke(tether);
        onDialogBoxEnd.Invoke();
        
        Progress(_currentDialog._nexts[0]);
    }

    public void BranchChosen(int pathIndex)
    {
        if(_currentDialog._nexts.Length-1 < pathIndex) BranchChosen();
        
        DialogTether tether = _currentDialog._nexts[pathIndex];
        if (tether._destination == null)
        {
            onPathChosen.Invoke(null);
            TerminateDialog();
            return;
        }
        
        onPathChosen.Invoke(tether);
        onDialogBoxEnd.Invoke();

        Progress(_currentDialog._nexts[pathIndex]);
    }

    public void Progress(DialogTether tether)
    {
        if (tether == null)
        {
            Debug.LogError(
                "Tether on Progress method is null. This is an unexpected state and will terminate the dialog to protect any additional scripts.");
            TerminateDialog();
            return;
        }

        onProgress.Invoke(tether);

        if (_currentDialog._shouldFireScriptEvents)
        {
            _currentDialog._scriptEvents.Invoke();
            onDialogBoxScriptFire.Invoke();
        }

        if (tether._destination == null)
        {
            TerminateDialog();
            return;
        }

        SetDialogBox(tether._destination);
    }

    public void TerminateDialog()
    {
        if (!_tree)
        {
            Debug.LogError("There is no dialog running. You cannot terminate one.");
            return;
        }

        _tree = null;
        _currentDialog = null;
        onDialogEnd.Invoke();
    }
}
