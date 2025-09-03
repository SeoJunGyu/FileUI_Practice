using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationTest))] //해당 클래스의 인스펙터 노출을 오버라이드한다는 의미이다.
public class LocalizationTextEditor : Editor
{
    //인스펙터 GUI 갱신 함수다. / 마우스 클릭처럼 특정 포커스나 입력 이벤트가 있을때만 호출되는 함수다.
    public override void OnInspectorGUI()
    {
        //target : 해당 클래스의 컴포넌트를 의미한다.
        var text = target as LocalizationTest;
        var newId = EditorGUILayout.TextField("String ID", text.stringId); //스트링을 그려주고 읽는 함수다. / (라벨, 내용)
        text.stringId = newId;
        var newLang = (Languages)EditorGUILayout.EnumPopup("Language", text.editorLang); //열거형을 그려주고 읽는 함수다. / 열거형은 System.enum 형으로 반환되기 때문에 형변환이 필수다.

        //아이디나 언어가 바뀌었다면
        if(newId != text.stringId || newLang != text.editorLang)
        {
            text.stringId = newId;
            text.editorLang = newLang;

            text.OnChangeLanguage(text.editorLang);

            EditorUtility.SetDirty(text); //갱신할 객체를 설정하는 함수다. / 사용 이유 : 여기만 갱신되고 다른곳은 갱신이 안되기 때문이다. / 에디터상에서 제대로 갱신되도록 하려하기 위해서다.
            //더티 플래그를 할당받은 객체만 갱신이 된다. -> 즉, 이 함수는 더티 플래그를 할당하는 것이다.
            //이걸 안하면 에디터에서 바꿨는데도 바뀌지 않는 오류가 있다.
        }
    }
}

[CustomEditor(typeof(ItemLocalization))] //해당 클래스의 인스펙터 노출을 오버라이드한다는 의미이다.
public class ItemLocalizationTextEditor : Editor
{
    //인스펙터 GUI 갱신 함수다. / 마우스 클릭처럼 특정 포커스나 입력 이벤트가 있을때만 호출되는 함수다.
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var text = target as ItemLocalization;

        EditorGUI.BeginChangeCheck();
        var newId = EditorGUILayout.TextField("String ID", text.stringId); //스트링을 그려주고 읽는 함수다. / (라벨, 내용)

        //아이디나 언어가 바뀌었다면
        if (EditorGUI.EndChangeCheck())
        {
            text.stringId = newId;

            text.OnChangeItem();

            EditorUtility.SetDirty(text); //갱신할 객체를 설정하는 함수다. / 사용 이유 : 여기만 갱신되고 다른곳은 갱신이 안되기 때문이다. / 에디터상에서 제대로 갱신되도록 하려하기 위해서다.
            //더티 플래그를 할당받은 객체만 갱신이 된다. -> 즉, 이 함수는 더티 플래그를 할당하는 것이다.
            //이걸 안하면 에디터에서 바꿨는데도 바뀌지 않는 오류가 있다.
        }
    }
}
