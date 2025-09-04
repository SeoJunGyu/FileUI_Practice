using System;
using UnityEngine;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };

    public static readonly string ItemTableId = "ItemTable";

    public static string String => StringTableIds[(int)Variables.Language]; //��� ���� / ������Ƽ�� �ٲ� ���� : DataTableManager���� ������ id�� ������������ id�� �����ϱ� ���ؼ���.
    public static readonly string Item = "ItemTable";
}

public static class Variables
{
    public static Languages Language = Languages.Korean;
}
