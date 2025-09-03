using System.Collections.Generic;
using UnityEngine;

public class ItemTable : DataTable
{
    public static readonly Data Unknown = null;
    public class Data
    {
        public string Id {  get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Category {  get; set; }
        public string Value {  get; set; }
        public string Price {  get; set; }
        public string Explane {  get; set; }
    }

    private readonly Dictionary<string, Data> dictionary = new Dictionary<string, Data>();

    public override void Load(string filename)
    {
        dictionary.Clear();

        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<Data>(textAsset.text);

        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id))
            {
                dictionary.Add(item.Id, item);
            }
            else
            {
                Debug.Log($"Å° Áßº¹: {item.Id}");
            }
        }
    }

    public Data Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            return Unknown;
        }

        return dictionary[key];
    }
}
