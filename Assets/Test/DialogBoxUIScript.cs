using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxUIScript : MonoBehaviour
{
    public static DialogBoxUIScript instance;

    private void Awake()
    {
        if (instance != null) throw new Exception("Cannot have more then one DialogBoxUiScript");
        instance = this;
    }

    public Text mainText;
    public GameObject _chooserContentGameObject;
    public GameObject _optionGameObjectPrefab;
    
    public DialogTree DialogTree;
    private DialogManager _dialogManager;

    private bool isInDialog = false;

    private void Start()
    {
        _dialogManager = DialogManager.instance;
        _dialogManager.onDialogBegin.AddListener(OnDialogBegin);
        _dialogManager.onDialogEnd.AddListener(OnDialogEnd);
        _dialogManager.onDialogBoxBegin.AddListener(OnDialogBoxBegin);
        _dialogManager.onDialogBoxEnd.AddListener(OnDialogBoxEnd);
        _dialogManager.onBranchDetected.AddListener(OnBranchDetected);
        _dialogManager.onProgress.AddListener(OnProgress);
        _dialogManager.onPathChosen.AddListener(OnPathChosen);
    }
    
    public void StartDialogButton()
    {
        if(!isInDialog)
        {
            ClearDialogBox();
            _dialogManager.StartDialog(DialogTree);
        }
        
    }
    
    public void NextDialogButton()
    {
        if(isInDialog)
            _dialogManager.BranchChosen();
    }
    
    public void EndDialogButton()
    {
        if(isInDialog)
            _dialogManager.TerminateDialog();
    }

    public void OnDialogBegin(DialogTree tree)
    {
        Debug.Log("Dialog begin.");
        isInDialog = true;
    }

    public void OnDialogEnd()
    {
        Debug.Log("Dialog end.");
        isInDialog = false;
        
        ClearDialogBox();
    }
    
    public void OnDialogBoxBegin(DialogBase dialog)
    {
        Debug.Log("Current Dialog begins.");
        mainText.text = dialog._text;
    }

    public void OnDialogBoxEnd()
    {
        Debug.Log("Current Dialog ends.");
        ClearDialogBox();
    }
    
    public void OnBranchDetected(DialogTether[] tethers)
    {
        Debug.Log("Multiple Branches detected.");
        for (int i = 0; i < tethers.Length; i++)
        {
            GameObject optionGameObject =
                Instantiate(_optionGameObjectPrefab, _chooserContentGameObject.transform, false);
            optionGameObject.GetComponent<ChooserScript>()._index = i;
            optionGameObject.GetComponent<ChooserScript>().SetText(tethers[i]._text);
        }
    }

    public void OnProgress(DialogTether tether)
    {
        Debug.Log("Dialog Progressing.");
    }

    public void OnPathChosen(DialogTether tether)
    {
        Debug.Log("Path has been chosen.");
    }

    public void Choose(int index)
    {
        _dialogManager.BranchChosen(index);
    }

    public void ClearDialogBox()
    {
        mainText.text = "";
        ClearOptionsGameObject();
    }
    
    public void ClearOptionsGameObject()
    {
        foreach (Transform child in _chooserContentGameObject.transform)
        {
            Destroy(child.gameObject);   
        }
    }

    public IEnumerator ChooseEnumerator(int index)
    {
        yield return null;
        _dialogManager.BranchChosen(index);
    }
}