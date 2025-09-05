using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class SaveLoadManage
{
    public static int SaveDataVersion { get; } = 1;

    public static SaveDataV1 Data { get; set; } //���̺� ������ ��ü ����

    private static readonly string[] SaveFileName =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    public static string SaveDirectory => $"{Application.persistentDataPath}/Save"; //���̺����� ���� ���

    //Format, TypeNameHandling, Converter�� �̸� ������ �� �ִ�.
    public static JsonSerializerSettings settings = new JsonSerializerSettings() 
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.Auto,
    };

    public static bool Save(int slot = 0)
    {
        if(Data == null || slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }

        //Directory.Exists : �ش� ��ΰ� �ִ��� ��ȯ�ϴ� �Լ�
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]); //���̺����� ���� ���
        var json = JsonConvert.SerializeObject(Data, Formatting.Indented);
        File.WriteAllText(path, json); //������ ����ó���� �ʿ��ϴ�.

        return true;
    }

    public static bool Load(int slot = 0)
    {
        //�ε�� �����Ͱ� null�̾ ��� ����.
        if (slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        if (!File.Exists(path))
        {
            return false;
        }

        var json = File.ReadAllText(path);

        //���� �� ���̿� �̷� ���� �ϵ��� ������Ѵ�.

        Data = JsonConvert.DeserializeObject<SaveDataV1>(json, settings);

        return true;
    }
}
