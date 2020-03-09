using UnityEngine;

///<summary>This is just an asset template for a Yes No Dialog Node.</summary>
[CreateAssetMenu(menuName = "Dialog/Dialog Types/Yes No Dialog")]
public class YesNoDialogNode : BasicDialogNode
{
    private void Awake()
    {
        base._nexts = new [] {new DialogTether("Yes"), new DialogTether("No") };
    }
}
