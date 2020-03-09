using UnityEngine;

/**
 * This is just an asset template for an Okay Dialog Node.
 */
[CreateAssetMenu(menuName = "Dialog/Dialog Types/Okay Dialog")]
public class OkayDialogNode : BasicDialogNode
{
    public OkayDialogNode() : base()
    {
        _nexts = new[] {new DialogTether("Okay")};
    }

    public OkayDialogNode(string okayMessage, BasicDialogNode okayDestination) : base()
    {
        _nexts = new[] {new DialogTether(okayMessage, okayDestination)};
    }
}
