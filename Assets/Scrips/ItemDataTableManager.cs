using System.Collections.Generic;
using UnityEngine;

public class ItemDataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static ItemDataTableManager()
    {
        Init();
    }

    //��� ������ ���̺� ��������
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
            Debug.LogError("���̺� ����");
            return null;
        }

        return tables[id] as T;
    }
}
