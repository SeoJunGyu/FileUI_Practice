using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{
    public static readonly string Unknown = "Ű����";
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    //readonly�� ���� �� �ִ°��� ��� �ٿ��� -> ������ �÷����� �̰ſ� �۵����� �ʰԵȴ�. / readonly����ؼ� 
    private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public override void Load(string fileame)
    {
        dictionary.Clear(); //����ִ� ���·� ����� ���� ä��� ���̴�.

        var path = string.Format(FormatPath, fileame); //���� ���
        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<Data>(textAsset.text);
        foreach( var item in list)
        {
            if (!dictionary.ContainsKey(item.Id)) 
            {
                dictionary.Add(item.Id, item.String);
            }
            else
            {
                Debug.LogError($"Ű �ߺ�: {item.Id}");
            }
        }
    }

    public string Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            return Unknown;
        }

        return dictionary[key];
    }
}
