using System.Collections.Generic;
using UnityEngine;

public class ItemDataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static ItemDataTableManager()
    {
        Init();
    }

    //모든 데이터 테이블 가져오기
    private static void Init()
    {
        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.DefaultItem);
        tables.Add(DataTableIds.DefaultItem, itemTable);
    }

    public static ItemTable ItemTable
    {
        get
        {
            return Get<ItemTable>(DataTableIds.DefaultItem);
        }
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }

        return tables[id] as T;
    }
}
