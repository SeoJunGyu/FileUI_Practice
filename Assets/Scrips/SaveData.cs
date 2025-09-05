using System;
using UnityEngine;

[Serializable]
public abstract class SaveData
{
    public int Version {  get; protected set; } //해당 세이브파일 버전

    public abstract SaveData VersionUp(); //상속받은 자식들 버전을 윗 버전으로 올리는 메서드
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
