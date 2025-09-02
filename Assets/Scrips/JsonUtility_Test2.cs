using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

public class JsonUtility_Test2 : MonoBehaviour
{
    public static readonly string fileName = "cube.json";
    public static string FileFullPath => Path.Combine(Application.persistentDataPath, fileName);

    public GameObject target;

    public void Save()
    {
        var json = JsonConvert.SerializeObject(target.transform.position, new Vector3Converter());
        File.WriteAllText(FileFullPath, json); //포지션 저장
    }

    public void Load()
    {
        //if (!Directory.Exists(FileFullPath)) return;
        //if (!File.Exists(FileFullPath)) return;

        var json = File.ReadAllText(FileFullPath);
        var position = JsonConvert.DeserializeObject<Vector3>(json, new Vector3Converter());

        target.transform.position = position;
    }
}
