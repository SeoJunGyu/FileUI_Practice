using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI textCursor;
    private int textCount = 0;

    public Button btnCancel;
    public Button btnDelete;

    public List<Button> buttons;

    private StringBuilder cursorStr = new StringBuilder("_", 11);
    private string text;

    private Coroutine cursorCorutine;

    private void Awake()
    {
        foreach(var btn in buttons)
        {
            var text = btn.GetComponentInChildren<TextMeshProUGUI>().text;
            btn.onClick.AddListener(() => OnKey(text));
        }

        btnCancel.onClick.AddListener(OnCancel);
        btnDelete.onClick.AddListener(OnDelete);
    }

    public override void Open()
    {
        base.Open();

        if (cursorCorutine != null)
        {
            StopCoroutine(cursorCorutine);
        }
        cursorCorutine = StartCoroutine(CoCursor());
    }

    public void OnKey(string key)
    {
        text += key;

        if(textCount < 8)
        {
            cursorStr = new StringBuilder(text);
            textCursor.text = cursorStr.ToString();
            textCount++;
        }
        
    }

    public void OnDelete()
    {
        if(textCount > 0)
        {
            cursorStr.Remove(cursorStr.Length - 1, 1);
            text = cursorStr.ToString();
            textCursor.text = text;
            textCount--;
        }
    }

    public void OnCancel()
    {
        textCursor.text = "";
        cursorStr.Remove(0, cursorStr.Length);
        textCount = 0;
        Debug.Log(cursorStr.Length);
    }

    public IEnumerator CoCursor()
    {
        if(textCount > 11)
        {
            yield return null;
        }

        cursorStr.Replace(cursorStr[textCount], cursorStr[textCount] == '_' ? ' ' : '_', textCount, 1);

        textCursor.text = cursorStr.ToString();

        yield return new WaitForSeconds(0.5f);
    }
}
