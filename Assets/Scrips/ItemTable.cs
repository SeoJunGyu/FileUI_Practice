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

    public string StringName => DataTableManager.StringTable.Get(Name); //실제 string 리턴
    public string StringDesc => DataTableManager.StringTable.Get(Desc);

    //추천 -> 한번 해놓고 계속 사용하는게 좋지만 언어 바뀌면 계속 초기화 해줘야한다. / 지금은 간단하게 구현
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}"); //원래는 리터럴 이렇게 사용하면 안된다.
}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> dictionary = new Dictionary<string, ItemData>();

    public override void Load(string filename)
    {
        dictionary.Clear();

        var path = string.Format(FormatPath, filename); //테이블 파일 경로 저장
        var textAsset = Resources.Load<TextAsset>(path); //실제 테이블 에셋 가져오기
        var list = LoadCSV<ItemData>(textAsset.text); //테이블 데이터를 리스트로 저장

        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id))
            {
                dictionary.Add(item.Id, item);
            }
            else
            {
                Debug.LogError($"키 중복: {item.Id}");
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
