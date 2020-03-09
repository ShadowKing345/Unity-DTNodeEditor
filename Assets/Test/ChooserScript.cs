using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script that is inside the options prefab.
/// </summary>
public class ChooserScript : MonoBehaviour
{
    /// <summary>
    /// The main text ui component of the options prefab.
    /// </summary>
    [SerializeField] private Text _mainText;
    /// <summary>
    /// The DialogBoxUIScript used. 
    /// </summary>
    private DialogBoxUIScript _parentScript;
    /// <summary>
    /// The index of the dialog tether.
    /// </summary>
    private int _index;
    
    public void Start()
    {
        _parentScript = DialogBoxUIScript.instance;
    }

    /// <summary>
    /// This is called when the button in the options prefab is clicked.
    /// </summary>
    public void OnChooserButtonClick()
    {
#if DEBUG
        Debug.Log("Option " + _mainText.text + " has been chosen.");
#endif
        StartCoroutine(_parentScript.ChooseEnumerator(_index));
        Destroy(gameObject);
    }

    /// <summary>
    /// This method sets the text Ui component.
    /// </summary>
    /// <param name="text">The string that will be set.</param>
    /// <param name="index">The index of the dialog tether.</param>
    public void Set(string text, int index)
    {
        _mainText.text = text;
        _index = index;
    }
}
