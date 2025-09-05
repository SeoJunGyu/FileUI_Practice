using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    private void OnEnable()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.text = string.Empty;
        textDescription.text = string.Empty;
        textCost.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
    }

    public void SetItem(SaveItemData data)
    {
        icon.sprite = data.itemData.SpriteIcon;
        textName.text = data.itemData.StringName;
        textDescription.text = data.itemData.StringDesc;
        textType.text = data.itemData.Type.ToString();
        textValue.text = data.itemData.Value.ToString();
        textCost.text  = data.itemData.Cost.ToString();
    }
}
