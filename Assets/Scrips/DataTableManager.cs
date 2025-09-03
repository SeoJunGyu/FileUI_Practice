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

    //��� ������ ���̺� ��������
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
            stringTable.Load(DataTableIds.String); //���� ������ ��� ���̺� �ε�
            tables.Add(DataTableIds.String, stringTable);
        }
        else
        {
            var table = new StringTable();
            stringTable.Load(DataTableIds.DefaultItem); //���� ������ ��� ���̺� �ε�
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
            Debug.LogError("���̺� ����");
            return null;
        }

        return tables[id] as T;
    }
}
