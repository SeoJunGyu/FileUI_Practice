using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

public class JsonUtility_Test1 : MonoBehaviour
{
    private void Start()
    {
        var obj = new PlayerState
        {
            playerName = "ABC",
            lives = 10,
            health = 10.999f,
            array = new int [] {9, 8, 7, 6}
        };

        //var json = obj.SaveToString();
        //var json = JsonUtility.ToJson(obj); //overwrite�� ��ü�� �ϳ� �����ͼ� ����°��̴�.
        //Debug.Log(json);

        //var obj2 = JsonUtility.FromJson<PlayerState>(json);
        //Debug.Log(obj2);

        //������ �ؽ�Ʈ ������ ��°�� �а� ���� �۾��� �߰��� ���ش�.
        //��θ� �����Ҷ��� �⺻ ��� ���� Ư�� ��Ѥ� ����ؾ��Ѵ�. => Application.persistentDataPath
        //���� : � �÷����̵� ���������� ������ �� �ֵ��� �ϱ� �����̴�.
        var path = Path.Combine(Application.persistentDataPath, "test.json");
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented); //��ü�� Json ���ڿ��� ��ȯ�ϴ� �Լ���. / Formattion.Indented : �鿩����
        File.WriteAllText(path, json);

        var json2 = File.ReadAllText(path); //���� ��ο��� string�� ��ȯ�ϴ� �Լ�
        var obj2 = JsonConvert.DeserializeObject<PlayerState>(json2);
        Debug.Log(obj2);
    }
}
