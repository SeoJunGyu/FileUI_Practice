using System;
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
        throw new System.NotImplementedException();
    }
}
