using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonRecordTest : MonoBehaviour
{
    public static readonly string fileName = "record.json";
    public static string FileFullPath => Path.Combine(Application.persistentDataPath, fileName);

    public GameObject target;

    List<SaveDatas> saves = new List<SaveDatas>();

    public float interval = 1f;

    public bool IsRecord { get; set; }

    private void FixedUpdate()
    {
        
    }

    public void Record()
    {
        IsRecord = !IsRecord;
        if (IsRecord)
        {

        }
        else
        {
            var json = JsonConvert.SerializeObject(saves, Formatting.Indented, new Vector3Converter());
            File.WriteAllText(FileFullPath, json); //포지션 저장
        }
            
    }

    public void Replay()
    {

        var json = File.ReadAllText(FileFullPath);
        var result = JsonConvert.DeserializeObject<List<SaveDatas>>(json, new Vector3Converter(), new QuaternionConverter(), new ColorConverter());
    }

    
}
