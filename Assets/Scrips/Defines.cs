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

    public static string String => StringTableIds[(int)Variables.Language]; //언어 선택 / 프로퍼티로 바꾼 이유 : DataTableManager에서 고정된 id와 고정되지않은 id를 구분하기 위해서다.
    public static string DefaultItem => StringTableIds[Array.IndexOf(StringTableIds, "ItemTable")];
}

public static class Variables
{
    public static Languages Language = Languages.Korean;
    public static Items Item = Items.Sword;
}
