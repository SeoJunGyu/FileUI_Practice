using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiInvenSlotList : MonoBehaviour
{
    public InvenSlot prefab;

    public ScrollRect scrollRect;

    private List<InvenSlot> slotList = new List<InvenSlot>(); //���ۺ��� �����ϴ� ����̶�� �������ڸ��� �����ؼ� SetEmpty�� �Ҵ��Ѵ�.

    public int maxCount = 30; //���� ���԰��� ������ �ʿ��ϸ� ����ȴ�.
    private int itemCount = 0; //���� ������ ��

    private List<SaveItemData> testItemList = new List<SaveItemData>(); //������ ���� ����

    private void Awake()
    {
        for (int i = 0; i < maxCount; ++i)
        {
            var newSlot = Instantiate(prefab, scrollRect.content);
            newSlot.slotIndex = i;
            newSlot.SetEmpty();
            newSlot.gameObject.SetActive(false);
            slotList.Add(newSlot);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RemoveItem(0);
        }
    }

    private void UpdateSlots(List<SaveItemData> itemList)
    {
        if (slotList.Count < itemList.Count)
        {
            for (int i = slotList.Count; i < itemList.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content); //UI��ü�� �����Ҷ��� �θ� ������Ʈ Ʈ�������� �߿��ϴ�. / �θ� ������Ʈ ����
                newSlot.slotIndex = i; //���� ��ȣ
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);
                slotList.Add(newSlot);
            }
        }
        for (int i = 0; i < slotList.Count; ++i)
        {
            if (i < itemList.Count)
            {
                slotList[i].gameObject.SetActive(true);
                slotList[i].SetItem(itemList[i]);
            }
            else
            {
                slotList[i].SetEmpty();
                slotList[i].gameObject.SetActive(false);
            }
        }
    }

    /*
    public void AddRandomItem()
    {
        var itemData = DataTableManager.ItemTable.GetRandom();
        var newSlot = Instantiate(prefab, scrollRect.content); //UI��ü�� �����Ҷ��� �θ� ������Ʈ Ʈ�������� �߿��ϴ�. / �θ� ������Ʈ ����
        slotList.Add(newSlot);
        newSlot.SetItem(itemData);
    }
    */

    public void AddRandomItem()
    {
        //var itemData = DataTableManager.ItemTable.GetRandom();
        //testItemList.Add(itemData);

        var itemInstance = new SaveItemData();
        itemInstance.itemData = DataTableManager.ItemTable.GetRandom();
        testItemList.Add(itemInstance);
        UpdateSlots(testItemList);
    }

    public void RemoveItem(int slotIndex)
    {
        testItemList.RemoveAt(slotIndex);
        UpdateSlots(testItemList);
    }
}
