using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class SaveDatas
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Color color;
}

public class JsonTest : MonoBehaviour
{
    public static readonly string fileName = "cube.json";
    public static readonly string colorFileName = "color.json";
    public static string FileFullPath => Path.Combine(Application.persistentDataPath, fileName);
    public static string ColorFileFullPath => Path.Combine(Application.persistentDataPath, colorFileName);

    public GameObject target;
    Renderer cubeColor;

    public static List<GameObject> saveData = new List<GameObject>();
    List<SaveDatas> saves = new List<SaveDatas>();

    private void Start()
    {
        cubeColor = target.GetComponent<Renderer>();
    }

    public void RandomObject()
    {
        for (int i = 0; i < 10; i++)
        {
            var obj = Instantiate(target);
            obj.transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-4f, 7f), UnityEngine.Random.Range(-5f, 20f));
            obj.transform.rotation = UnityEngine.Random.rotation;
            obj.transform.localScale = new Vector3(UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f));

            var objRenderer = obj.GetComponent<Renderer>();
            objRenderer.material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));

            saveData.Add(obj);
        }
    }

    public void Save()
    {
        saves.Clear();

        foreach(var item in saveData)
        {
            SaveDatas data = new SaveDatas();
            data.position = item.transform.position;
            data.rotation = item.transform.rotation;
            data.scale = item.transform.localScale;

            var objRenderer = item.GetComponent<Renderer>();
            data.color = objRenderer.material.color;

            saves.Add(data);
        }
        var json = JsonConvert.SerializeObject(saves, Formatting.Indented, new Vector3Converter(), new QuaternionConverter(), new ColorConverter());
        File.WriteAllText(FileFullPath, json); //포지션 저장
    }

    public void Load()
    {
        //if (!Directory.Exists(FileFullPath)) return;
        //if (!File.Exists(FileFullPath)) return;

        foreach(var item in saveData)
        {
            if(item != null)
            {
                Destroy(item);
            }
        }

        saveData.Clear();

        var json = File.ReadAllText(FileFullPath);
        var result = JsonConvert.DeserializeObject<List<SaveDatas>>(json, new Vector3Converter(), new QuaternionConverter(), new ColorConverter());

        foreach(var objdata in result)
        {
            var obj = Instantiate(target);
            obj.transform.position = objdata.position;
            obj.transform.rotation = objdata.rotation;
            obj.transform.localScale = objdata.scale;

            var objRenderer = obj.GetComponent<Renderer>();
            objRenderer.material.color = objdata.color;

            saveData.Add(obj); 
        }

        //Instantiate(obj, obj.transform.position, obj.transform.rotation);
    }
}
