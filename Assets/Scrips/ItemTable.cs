using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public class ItemData
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc {  get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon {  get; set; }

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
    }

    public string StringName => DataTableManager.StringTable.Get(Name); //���� string ����
    public string StringDesc => DataTableManager.StringTable.Get(Desc);

    //��õ -> �ѹ� �س��� ��� ����ϴ°� ������ ��� �ٲ�� ��� �ʱ�ȭ ������Ѵ�. / ������ �����ϰ� ����
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}"); //������ ���ͷ� �̷��� ����ϸ� �ȵȴ�.
}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> dictionary = new Dictionary<string, ItemData>();

    public override void Load(string filename)
    {
        dictionary.Clear();

        var path = string.Format(FormatPath, filename); //���̺� ���� ��� ����
        var textAsset = Resources.Load<TextAsset>(path); //���� ���̺� ���� ��������
        var list = LoadCSV<ItemData>(textAsset.text); //���̺� �����͸� ����Ʈ�� ����

        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id))
            {
                dictionary.Add(item.Id, item);
            }
            else
            {
                Debug.LogError($"Ű �ߺ�: {item.Id}");
            }
        }

        foreach(var item in dictionary)
        {
            Debug.Log(item.Value);

            var data = item.Value;
            Debug.Log(data.StringName);
            Debug.Log(data.StringDesc);
            Debug.Log(data.SpriteIcon);
        }
    }

    public ItemData Get(string id)
    {
        if (!dictionary.ContainsKey(id))
        {
            return null;
        }

        return dictionary[id];
    }

    public ItemData GetRandom()
    {
        var itemList = dictionary.Values.ToList();
        return itemList[Random.Range(0, itemList.Count)];
    }
}
