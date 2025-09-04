using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    public int slotIndex { get; set; } //���� ���õǾ����� �˱����� ����
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

    //���� ����
    public void SetEmpty()
    {
        ItemData = null;
        imageIcon.sprite = null; //null�̸� �ƹ��͵� �ȱ׷�����.
        textName.text = string.Empty;
    }

    //���� ä���
    public void SetItem(SaveItemData data)
    {
        ItemData = data;
        imageIcon.sprite = ItemData.itemData.SpriteIcon;
        textName.text = ItemData.itemData.StringName;
    }
}
