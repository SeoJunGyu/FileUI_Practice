using TMPro;
using UnityEngine;

public class StringTableTest : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI textMeshPro;
    private void Start()
    {
        //var stringTable = DataTableManager.Get<StringTable>("String");
        //textMeshPro.text = stringTable.Get(id);


        textMeshPro.text = DataTableManager.StringTable.Get(id);
    }
}
