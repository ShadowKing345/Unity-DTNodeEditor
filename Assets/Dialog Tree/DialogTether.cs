using System;

/**
 * This is the object used to point where the next node is.
 */
[Serializable]
public class DialogTether
{
    //Used if the Dialog Manage is suppose to show a branch. Leave empty to not show. 
    public string _text;
    //The destination Dialog Node.
    public BasicDialogNode _destination;

    public DialogTether(string text)
    {
        _text = text;
    }
    
    public DialogTether(string text, BasicDialogNode destination)
    {
        _text = text;
        _destination = destination;
    }

    public DialogTether(BasicDialogNode destination)
    {
        _text = "";
        _destination = destination;
    }
}
