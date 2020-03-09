using UnityEngine;

/**
 * This is just an asset template for an Yes or No Dialog Node.
 */
[CreateAssetMenu(menuName = "Dialog/Dialog Types/Yes No Dialog")]
public class YesNoDialogNode : BasicDialogNode
{
    private void Awake()
    {
        base._nexts = new [] {new DialogTether("Yes"), new DialogTether("No") };
    }
}
