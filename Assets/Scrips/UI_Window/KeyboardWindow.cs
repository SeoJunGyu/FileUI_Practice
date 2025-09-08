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

    private StringBuilder cursorStr = new StringBuilder();
    private string text;
    private string textUnderBar = "_";
    private string textBefore;
    private bool showCursor = true;

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

    public override void Close()
    {
        if (cursorCorutine != null)
        {
            StopCoroutine(cursorCorutine);
        }
        cursorCorutine = null;

        base.Close();
    }

    public void OnKey(string key)
    {
        if(textCount < 8)
        {
            text += key;
            cursorStr = new StringBuilder(text);
            textCursor.text = cursorStr.ToString();
            textCount++;
        }
        Debug.Log(textCount);
        RefreshDisplay();
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

        RefreshDisplay();
    }

    public void OnCancel()
    {
        cursorStr.Remove(0, cursorStr.Length);
        text = cursorStr.ToString();
        textCursor.text = text;
        textCount = 0;
    }

    public IEnumerator CoCursor()
    {
        while (true)
        {
            if (textCount < 8)
            {
                showCursor = !showCursor;
                textCursor.text = showCursor ? $"{text}_" : $"{text} ";
            }
            else
            {
                textCursor.text = text;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void RefreshDisplay()
    {
        if(text.Length < 8)
        {
            textCursor.text = showCursor ? $"{text}_" : $"{text} ";
        }
        else
        {
            textCursor.text = text;
        }
    }
}
