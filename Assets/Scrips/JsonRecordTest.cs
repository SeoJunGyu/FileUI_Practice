using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Record
{
    public Vector3 position;
    public Quaternion rotation;
}

public class JsonRecordTest : MonoBehaviour
{
    public static readonly string fileName = "record.json";
    public static string FileFullPath => Path.Combine(Application.persistentDataPath, fileName);

    public GameObject target;

    List<Record> saves = new List<Record>();

    public float interval = 1f;
    private float timer = 0f;

    public bool IsRecord { get; set; }

    public GameObject startReplay;
    public GameObject stopReplay;

    Coroutine replayHandle;

    private void FixedUpdate()
    {
        
    }

    public void RecordData()
    {
        IsRecord = !IsRecord;
        if (IsRecord)
        {
            StartCoroutine(CoRecord());
        }
        else
        {
            StopCoroutine(CoRecord());

            var json = JsonConvert.SerializeObject(saves, Formatting.Indented, new Vector3Converter(), new QuaternionConverter());
            File.WriteAllText(FileFullPath, json); //포지션 저장
        }
            
    }

    public void LoadReplayData()
    {

        var json = File.ReadAllText(FileFullPath);
        saves = JsonConvert.DeserializeObject<List<Record>>(json, new Vector3Converter(), new QuaternionConverter());
    }

    public void StartReplay()
    {
        startReplay.SetActive(false);
        stopReplay.SetActive(true);

        if(saves == null || saves.Count == 0)
        {
            LoadReplayData();
        }

        if(replayHandle != null)
        {
            StopCoroutine(replayHandle);
        }

        replayHandle = StartCoroutine(CoReplay());
    }

    public void StopReplay()
    {
        startReplay.SetActive(true);
        stopReplay.SetActive(false);

        if (replayHandle != null)
        {
            StopCoroutine(replayHandle);
            replayHandle = null;
        }
    }

    public IEnumerator CoRecord()
    {
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > interval)
            {
                var tmp = new Record()
                {
                    position = target.transform.position,
                    rotation = target.transform.rotation
                };
                saves.Add(tmp);

                timer = 0f;
            }
            yield return null;
        }
    }

    public IEnumerator CoReplay()
    {
        target.transform.SetPositionAndRotation(saves[0].position, saves[0].rotation);
        if(saves.Count == 1)
        {
            replayHandle = null;
            yield break;
        }

        for (int i = 0; i < saves.Count - 1; i++)
        {
            var a = saves[i];
            var b = saves[i + 1];

            timer = 0f;
            while(timer < interval)
            {
                timer += Time.deltaTime / interval;
                if(timer > interval)
                {
                    timer = 1f;
                }

                var pos = Vector3.Lerp(a.position, b.position, timer);
                var rot = Quaternion.Slerp(a.rotation, b.rotation, timer);
                target.transform.SetPositionAndRotation(pos, rot);
            }
            
            yield return null;
        }

        replayHandle = null;
    }

    
}
