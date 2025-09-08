using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using SaveDataVC = SaveDataV4;

public static class SaveLoadManager
{
    public static int SaveDataVersion { get; } = 4;

    public static SaveDataVC Data { get; set; } = new SaveDataVC(); //���̺� ������ ��ü ����

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
        TypeNameHandling = TypeNameHandling.All,
    };

    static SaveLoadManager()
    {
        Load();
    }

    public static bool Save(int slot = 0)
    {
        if(Data == null || slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }

        //�����ϰ� ���� ó��
        try
        {
            //Directory.Exists : �ش� ��ΰ� �ִ��� ��ȯ�ϴ� �Լ�
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var path = Path.Combine(SaveDirectory, SaveFileName[slot]); //���̺����� ���� ���
            var json = JsonConvert.SerializeObject(Data, settings);
            File.WriteAllText(path, json); //������ ����ó���� �ʿ��ϴ�.

            return true;
        }
        catch
        {
            Debug.LogError("Save ���� �߻�");
            return false;
        }
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

        try
        {
            var json = File.ReadAllText(path);
            var dataSave = JsonConvert.DeserializeObject<SaveData>(json, settings);
            
            while(dataSave.Version < SaveDataVersion) //����������� ������Ʈ �ݺ�
            {
                dataSave = dataSave.VersionUp();
            }
            Data = dataSave as SaveDataVC;

            return true;
        }
        catch
        {
            Debug.LogError("Load ���� �߻�");
            return false;
        }
    }
}
