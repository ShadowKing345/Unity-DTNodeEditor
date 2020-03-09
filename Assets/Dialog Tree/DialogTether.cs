using System;

[Serializable]
public class DialogTether
{
    public string _text;
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
