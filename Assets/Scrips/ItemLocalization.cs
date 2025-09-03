using TMPro;
using UnityEngine;

public class ItemLocalization : MonoBehaviour
{
    public string stringId;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI categoryText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI explaneText;

    private void OnEnable()
    {
        
    }

    public void OnChangeItem()
    {
        var stringTable = DataTableManager;
        var tmp = stringTable.Get(stringId);

        nameText.text = tmp;
    }
}
