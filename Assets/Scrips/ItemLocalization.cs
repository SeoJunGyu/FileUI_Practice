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
        var itemTable = DataTableManager.Get<ItemTable>(DataTableIds.ItemTableId);

        var data = itemTable.Get(Items.Sword.ToString());

        nameText.text = data.Name;
        categoryText.text = data.Category;
        valueText.text = data.Value;
        priceText.text = data.Price;
        explaneText.text = data.Explane;
    }
}
