using UnityEngine;
using UnityEngine.UI;

public class ChooserScript : MonoBehaviour
{
    [SerializeField] private Text _mainText;
    private DialogBoxUIScript _parentScript;
    public int _index;
    
    public void Start()
    {
        _parentScript = DialogBoxUIScript.instance;
    }

    
    public void OnChooserButtonClick()
    {
        Debug.Log("Option " + _mainText.text + " has been chosen.");
        StartCoroutine(_parentScript.ChooseEnumerator(_index));
        Destroy(gameObject);
    }

    public void SetText(string text)
    {
        _mainText.text = text;
    }
}
