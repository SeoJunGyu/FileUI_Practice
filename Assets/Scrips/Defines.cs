using System;
using UnityEngine;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public enum Items
{
    Sword,
    Bow,
    Heart,
    Amor,
    Helmet,
    Shield,
    Star,
    Gold,
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
        "ItemTable",
    };

    public static string String => StringTableIds[(int)Variables.Language]; //��� ���� / ������Ƽ�� �ٲ� ���� : DataTableManager���� ������ id�� ������������ id�� �����ϱ� ���ؼ���.
    public static string DefaultItem => StringTableIds[Array.IndexOf(StringTableIds, "ItemTable")];
}

public static class Variables
{
    public static Languages Language = Languages.Korean;
    public static Items Item = Items.Sword;
}
