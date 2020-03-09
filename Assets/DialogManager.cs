using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * <summary>
 * The script used to handel dialog trees.
 * It is meant to be a single instance script.
 * </summary>
 */
public class DialogManager : MonoBehaviour
{
    
    
    //Use this to get a single instance.
    public static DialogManager instance;

    //The tree in use.
    private DialogTree _tree;
    //The current Dialog Node active.
    private BasicDialogNode _currentBasicDialog;
    //A history of what Dialog nodes where there.
    public Stack<BasicDialogNode> HistoryQueue { get; } = new Stack<BasicDialogNode>();

    public void Awake()
    {
        //Ensure that the latest creation of Dialog manager is the current instance.
        instance = this;
    }
    
    [Serializable]
    public class DialogTreeEvent : UnityEvent<DialogTree> {}
    public DialogTreeEvent onDialogBegin;
    public UnityEvent onDialogEnd;

    [Serializable]
    public class DialogEvent : UnityEvent<BasicDialogNode> {}
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
    
    /**
     * <summary>This function starts the dialog.</summary>
     * <param name="tree">The tree will be used to store what is the current tree and to get the starting point node.</param>
     */
    public void StartDialog(DialogTree tree)
    {
        if (_tree)
        {
            Debug.LogError("A Dialog has already started. You cannot start a new one.");
            return;
        }

        HistoryQueue.Clear();
        
        _tree = tree;
        onDialogBegin.Invoke(tree);
     
        SetDialogBox(tree._startNode);
    }
    public void SetDialogBox(BasicDialogNode basicDialog)
    {
        if (basicDialog == null)
        {
            TerminateDialog(); 
            return;
        }

        if (HistoryQueue.Count != 0)
        {
            basicDialog._previous = HistoryQueue.Peek();
        }

        _currentBasicDialog = basicDialog;
        HistoryQueue.Push(basicDialog);
        onDialogBoxBegin.Invoke(basicDialog);

        CheckIfBranching(basicDialog._nexts);
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
        DialogTether tether = _currentBasicDialog._nexts[0];

        if (tether != null && tether._destination == null)
        {
            onPathChosen.Invoke(tether);
            TerminateDialog();
            return;
        }
        onPathChosen.Invoke(tether);
        onDialogBoxEnd.Invoke();
        
        Progress(_currentBasicDialog._nexts[0]);
    }
    
    public void BranchChosen(int pathIndex)
    {
        if(_currentBasicDialog._nexts.Length-1 < pathIndex) BranchChosen();
        
        DialogTether tether = _currentBasicDialog._nexts[pathIndex];
        if (tether._destination == null)
        {
            onPathChosen.Invoke(null);
            TerminateDialog();
            return;
        }
        
        onPathChosen.Invoke(tether);
        onDialogBoxEnd.Invoke();

        Progress(_currentBasicDialog._nexts[pathIndex]);
    }


    public void Progress(DialogTether tether)
    {
        if (tether == null)
        {
            Debug.LogError(
                "Tether on Progress method is null. This is an unexpected state and will terminate the Dialog to protect any additional scripts.");
            TerminateDialog();
            return;
        }

        onProgress.Invoke(tether);

        if (_currentBasicDialog._shouldFireScriptEvents)
        {
            _currentBasicDialog._scriptEvents.Invoke();
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
            Debug.LogError("There is no Dialog running. You cannot terminate one.");
            return;
        }
        
        _tree = null;
        _currentBasicDialog = null;
        HistoryQueue.Clear();
        onDialogEnd.Invoke();
    }
}
