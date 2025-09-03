using TMPro;
using UnityEngine;

//������ ��� ���� / �������߿��� ���� Ŭ������.
[ExecuteInEditMode] //�������ϴ��߿��� Awake Start Update�� �����ϰ��ϴ� ��Ʈ����Ʈ��.
[RequireComponent(typeof(TextMeshProUGUI))] //�ش� Ÿ���� �ݵ�� �Ҵ�ǰ��ϴ� ��Ʈ����Ʈ��. / �����Ϸ����ϸ� ����������.
public class LocalizationTest : MonoBehaviour
{
    public string stringId;
    
    //����Ƽ �����Ϳ����� �����ϴ� �ʵ��.
#if UNITY_EDITOR
    public Languages editorLang;
#endif

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    //�����Ҷ� �̷��� �ϸ� �ȴ�.
    private void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            OnChangeLanguage();
        }
        else
        {
            OnChangeLanguage(editorLang);
        }
#else
        OnChangeLanguage();
#endif
    }

    public void OnChangeLanguage()
    {
        var stringTable = DataTableManager.StringTable;
        text.text = stringTable.Get(stringId);
    }

#if UNITY_EDITOR
    public void OnChangeLanguage(Languages lang)
    {
        var tableId = DataTableIds.StringTableIds[(int)lang];
        var stringTable = DataTableManager.Get<StringTable>(tableId);
        text.text = stringTable.Get(stringId);
    }
#endif
}
