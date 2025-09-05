using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        saveData.Gold = 0;

        return saveData;
    }
}

[Serializable]
public class SaveDataV2 : SaveData
{
    public string Name {  get; set; } = string.Empty; //이전과 변경된거
    public int Gold; //새로 생긴거

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
    public string Name { get; set; } = string.Empty; //이전과 변경된거
    public int Gold; //새로 생긴거
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
