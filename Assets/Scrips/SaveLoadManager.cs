using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class SaveLoadManage
{
    public static int SaveDataVersion { get; } = 1;

    public static SaveDataV1 Data { get; set; } //세이브 데이터 객체 유지

    private static readonly string[] SaveFileName =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    public static string SaveDirectory => $"{Application.persistentDataPath}/Save"; //세이브파일 폴더 경로

    //Format, TypeNameHandling, Converter를 미리 세팅할 수 있다.
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

        //Directory.Exists : 해당 경로가 있는지 반환하는 함수
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]); //세이브파일 저장 경로
        var json = JsonConvert.SerializeObject(Data, Formatting.Indented);
        File.WriteAllText(path, json); //원래는 예외처리가 필요하다.

        return true;
    }

    public static bool Load(int slot = 0)
    {
        //로드는 데이터가 null이어도 상관 없다.
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

        //원래 이 사이에 이런 저런 일들을 해줘야한다.

        Data = JsonConvert.DeserializeObject<SaveDataV1>(json, settings);

        return true;
    }
}
