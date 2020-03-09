using System;

///<summary>This object is used to point where the next node is.</summary>
[Serializable]
public class DialogTether
{
    ///<summary>Used if the Dialog Manage is suppose to show a branch. Leave empty to not show.</summary> 
    public string _text;
    ///<summary>The destination Dialog Node.</summary>
    public BasicDialogNode _destination;

    #region Constructers
    /// <summary>
    /// Constructor of <c>DialogTether</c> used to preset the _text value.
    /// </summary>
    /// <param name="text">the string to set the _text variable</param>
    public DialogTether(string text)
    {
        _text = text;
    }
    
    /// <summary>
    /// Constructor of <c>DialogTether</c> used to preset the _text value and _destination node.
    /// </summary>
    /// <param name="text">the string to set the _text variable</param>
    /// <param name="destination">a <c>BasicDialogNode</c> that will be the destination.</param>
    public DialogTether(string text, BasicDialogNode destination)
    {
        _text = text;
        _destination = destination;
    }

    /// <summary>
    /// Constructor of <c>DialogTether</c> used to preset the _destination node.
    /// </summary>
    /// <param name="destination">a <c>BasicDialogNode</c> that will be the destination.</param>
    public DialogTether(BasicDialogNode destination)
    {
        _text = "";
        _destination = destination;
    }
    #endregion
}
