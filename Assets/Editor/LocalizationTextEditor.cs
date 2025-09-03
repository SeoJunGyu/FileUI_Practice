using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationTest))] //�ش� Ŭ������ �ν����� ������ �������̵��Ѵٴ� �ǹ��̴�.
public class LocalizationTextEditor : Editor
{
    //�ν����� GUI ���� �Լ���. / ���콺 Ŭ��ó�� Ư�� ��Ŀ���� �Է� �̺�Ʈ�� �������� ȣ��Ǵ� �Լ���.
    public override void OnInspectorGUI()
    {
        //target : �ش� Ŭ������ ������Ʈ�� �ǹ��Ѵ�.
        var text = target as LocalizationTest;
        var newId = EditorGUILayout.TextField("String ID", text.stringId); //��Ʈ���� �׷��ְ� �д� �Լ���. / (��, ����)
        text.stringId = newId;
        var newLang = (Languages)EditorGUILayout.EnumPopup("Language", text.editorLang); //�������� �׷��ְ� �д� �Լ���. / �������� System.enum ������ ��ȯ�Ǳ� ������ ����ȯ�� �ʼ���.

        //���̵� �� �ٲ���ٸ�
        if(newId != text.stringId || newLang != text.editorLang)
        {
            text.stringId = newId;
            text.editorLang = newLang;

            text.OnChangeLanguage(text.editorLang);

            EditorUtility.SetDirty(text); //������ ��ü�� �����ϴ� �Լ���. / ��� ���� : ���⸸ ���ŵǰ� �ٸ����� ������ �ȵǱ� �����̴�. / �����ͻ󿡼� ����� ���ŵǵ��� �Ϸ��ϱ� ���ؼ���.
            //��Ƽ �÷��׸� �Ҵ���� ��ü�� ������ �ȴ�. -> ��, �� �Լ��� ��Ƽ �÷��׸� �Ҵ��ϴ� ���̴�.
            //�̰� ���ϸ� �����Ϳ��� �ٲ�µ��� �ٲ��� �ʴ� ������ �ִ�.
        }
    }
}

[CustomEditor(typeof(ItemLocalization))] //�ش� Ŭ������ �ν����� ������ �������̵��Ѵٴ� �ǹ��̴�.
public class ItemLocalizationTextEditor : Editor
{
    //�ν����� GUI ���� �Լ���. / ���콺 Ŭ��ó�� Ư�� ��Ŀ���� �Է� �̺�Ʈ�� �������� ȣ��Ǵ� �Լ���.
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var text = target as ItemLocalization;

        EditorGUI.BeginChangeCheck();
        var newId = EditorGUILayout.TextField("String ID", text.stringId); //��Ʈ���� �׷��ְ� �д� �Լ���. / (��, ����)

        //���̵� �� �ٲ���ٸ�
        if (EditorGUI.EndChangeCheck())
        {
            text.stringId = newId;

            text.OnChangeItem();

            EditorUtility.SetDirty(text); //������ ��ü�� �����ϴ� �Լ���. / ��� ���� : ���⸸ ���ŵǰ� �ٸ����� ������ �ȵǱ� �����̴�. / �����ͻ󿡼� ����� ���ŵǵ��� �Ϸ��ϱ� ���ؼ���.
            //��Ƽ �÷��׸� �Ҵ���� ��ü�� ������ �ȴ�. -> ��, �� �Լ��� ��Ƽ �÷��׸� �Ҵ��ϴ� ���̴�.
            //�̰� ���ϸ� �����Ϳ��� �ٲ�µ��� �ٲ��� �ʴ� ������ �ִ�.
        }
    }
}
