using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static DataTableManager() 
    {
        Init();
    }

    //모든 데이터 테이블 가져오기
    private static void Init()
    {
#if UNITY_EDITOR
        foreach(var id in DataTableIds.StringTableIds)
        {
            if(id.GetType() == typeof(ItemTable))
            {
                var table = new ItemTable();
                table.Load(id);
                tables.Add(id, table);
            }
            else
            {
                var table = new StringTable();
                //table.Load("StringTable");
                table.Load(id);
                tables.Add(id, table);
            }
        }
#else
        if(id.GetType() == typeof(ItemTable))
        {
            var table = new ItemTable();
            stringTable.Load(DataTableIds.String); //현재 언어설정의 언어 테이블 로드
            tables.Add(DataTableIds.String, stringTable);
        }
        else
        {
            var table = new StringTable();
            stringTable.Load(DataTableIds.DefaultItem); //현재 언어설정의 언어 테이블 로드
            tables.Add(DataTableIds.DefaultItem, stringTable);
        }
#endif

    }

    public static StringTable StringTable
    {
        get
        {
            return Get<StringTable>(DataTableIds.String);
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
