using TMPro;
using UnityEngine;

public class CSVTest2 : MonoBehaviour
{
    public TextMeshProUGUI test;

    private void Start()
    {
        var table = new ItemTable();
        table.Load("ItemTable");

        //var stringTable = new StringTable();
        //stringTable.Load("StringTableKr");

        //test.text = stringTable.Get("HELLO");

        //Debug.Log(stringTable.Get("HELLO"));
        //Debug.Log(stringTable.Get("BYE"));
        //Debug.Log(stringTable.Get("YOU DIE"));
    }
}
