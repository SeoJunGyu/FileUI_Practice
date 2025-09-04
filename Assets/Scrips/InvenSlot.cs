using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    public int slotIndex { get; set; } //누가 선택되었는지 알기위해 선언
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData ItemData { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SetEmpty();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //var data = DataTableManager.ItemTable.Get("Item4");
            //SetItem(data);
        }
    }

    //슬롯 비우기
    public void SetEmpty()
    {
        ItemData = null;
        imageIcon.sprite = null; //null이면 아무것도 안그려진다.
        textName.text = string.Empty;
    }

    //슬롯 채우기
    public void SetItem(SaveItemData data)
    {
        ItemData = data;
        imageIcon.sprite = ItemData.itemData.SpriteIcon;
        textName.text = ItemData.itemData.StringName;
    }
}
