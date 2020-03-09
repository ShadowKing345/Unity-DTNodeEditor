using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The script used to handel dialog trees.
/// There is only meant to be a single instance of this script.
/// </summary>
public class DialogManager : MonoBehaviour
{

    ///<summary>The static object instance of this script</summary>
    public static DialogManager instance;

    /// <summary>
    /// The tree currently being used.
    /// </summary>
    private DialogTree _tree;

    /// <summary>
    /// The current active dialog node.
    /// </summary>
    private BasicDialogNode _currentBasicDialog;

    /// <summary>
    /// A history of the progression of dialog nodes.
    /// </summary>
    public Stack<BasicDialogNode> HistoryQueue { get; } = new Stack<BasicDialogNode>();

    public void Awake()
    {
        //Ensure that the latest creation of Dialog manager is the current instance.
        instance = this;
    }

    #region Events Variables

    [Serializable]
    public class DialogTreeEvent : UnityEvent<DialogTree>
    {
    }

    public DialogTreeEvent onDialogBegin;
    public UnityEvent onDialogEnd;

    [Serializable]
    public class DialogEvent : UnityEvent<BasicDialogNode>
    {
    }

    public DialogEvent onDialogBoxBegin;
    public UnityEvent onDialogBoxEnd;

    [Serializable]
    public class BranchDetectedClass : UnityEvent<DialogTether[]>
    {
    }

    public BranchDetectedClass onBranchDetected;

    [Serializable]
    public class DialogTetherEvent : UnityEvent<DialogTether>
    {
    }

    public DialogTetherEvent onPathChosen;
    public DialogTetherEvent onProgress;

    public UnityEvent onDialogBoxScriptFire;

    #endregion


    /// <summary>This function starts the dialog.</summary>
    ///<param name="tree">The tree will be used to store what is the current tree and to get the starting point node.</param>
    public void StartDialog(DialogTree tree)
    {
        if (_tree)
        {
#if DEBUG
            Debug.LogError("A Dialog has already started. You cannot start a new one.");
#endif
            return;
        }

        HistoryQueue.Clear();

        _tree = tree;
        onDialogBegin.Invoke(tree);

        SetDialogBox(tree._startNode);
    }

    /// <summary>
    /// This method sets the current dialog node being used.
    /// It also calls the method <c>CheckIfBranching</c>
    /// </summary>
    /// <param name="basicDialog">The Dialog Node to be set as current.</param>
    public void SetDialogBox(BasicDialogNode basicDialog)
    {
        if (basicDialog == null)
        {
            TerminateDialog();
            return;
        }

        //Sets the _previous variable of the dialog given.
        if (HistoryQueue.Count != 0)
            basicDialog._previous = HistoryQueue.Peek();


        _currentBasicDialog = basicDialog;
        HistoryQueue.Push(basicDialog);
        onDialogBoxBegin.Invoke(basicDialog);

        CheckIfBranching(basicDialog._nexts);
    }

    /// <summary>
    /// This method check if the <paramref name="tethers"/> from a dialog node is a branching or not.
    /// </summary>
    /// <param name="tethers">A list of tethers to be inspected</param>
    public void CheckIfBranching(DialogTether[] tethers)
    {
        if (tethers.Length > 1)
            onBranchDetected.Invoke(tethers);
        else if (!string.IsNullOrEmpty(tethers[0]._text) && !string.IsNullOrWhiteSpace(tethers[0]._text))
            onBranchDetected.Invoke(tethers);
    }

    /// <summary>
    /// This method progresses the dialog with the assumption that there is only a single unseen branch to be chosen.
    /// </summary>
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

    /// <summary>
    /// This method will progress the dialog along the <paramref name="pathIndex"/> branch.
    /// </summary>
    /// <param name="pathIndex">The index of the branch to follow.</param>
    public void BranchChosen(int pathIndex)
    {
        if (_currentBasicDialog._nexts.Length - 1 < pathIndex) BranchChosen();

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

    /// <summary>
    /// This method progresses the dialog to any given point with the <paramref name="tether"/> destination.
    /// </summary>
    /// <param name="tether">The <c>DialogTether</c> for the dialog to progress along.</param>
    public void Progress(DialogTether tether)
    {
        if (tether == null)
        {
            Debug.LogError("Tether on Progress method is null. This is an unexpected state and will terminate the Dialog to protect any additional scripts.");
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

    /// <summary>
    /// This method ends the dialog.
    /// </summary>
    /// <remarks>After this method is called some variable used will get cleared. The event are an exception to this.</remarks>
    public void TerminateDialog()
    {
        if (!_tree)
        {
#if DEBUG
            Debug.LogError("There is no Dialog running. You cannot terminate one.");
#endif
            return;
        }

        _tree = null;
        _currentBasicDialog = null;
        HistoryQueue.Clear();
        onDialogEnd.Invoke();
    }
}
