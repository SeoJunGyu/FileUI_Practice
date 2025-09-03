using TMPro;
using UnityEngine;

//실행중 언어 변경 / 에디터중에도 언어변경 클래스다.
[ExecuteInEditMode] //에디터하는중에도 Awake Start Update가 동작하게하는 어트리뷰트다.
[RequireComponent(typeof(TextMeshProUGUI))] //해당 타입을 반드시 할당되게하는 어트리뷰트다. / 삭제하려고하면 에러가난다.
public class LocalizationTest : MonoBehaviour
{
    public string stringId;
    
    //유니티 에디터에서만 동작하는 필드다.
#if UNITY_EDITOR
    public Languages editorLang;
#endif

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    //빌드할때 이렇게 하면 된다.
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
