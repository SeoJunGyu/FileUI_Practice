using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SaveData
{
    public int Version {  get; protected set; } //�ش� ���̺����� ����

    public abstract SaveData VersionUp(); //��ӹ��� �ڽĵ� ������ �� �������� �ø��� �޼���
}

[Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        saveData.Gold = 0;

        return saveData;
    }
}

[Serializable]
public class SaveDataV2 : SaveData
{
    public string Name {  get; set; } = string.Empty; //������ ����Ȱ�
    public int Gold; //���� �����

    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;

        return saveData;
    }
}

[Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty; //������ ����Ȱ�
    public int Gold; //���� �����
    public List<SaveItemData> items = new List<SaveItemData>();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        throw new NotImplementedException();
    }
}
