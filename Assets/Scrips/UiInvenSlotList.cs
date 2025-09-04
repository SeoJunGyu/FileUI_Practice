using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiInvenSlotList : MonoBehaviour
{
    public InvenSlot prefab;

    public ScrollRect scrollRect;

    private List<InvenSlot> slotList = new List<InvenSlot>(); //시작부터 생성하는 방식이라면 시작하자마자 생성해서 SetEmpty로 할당한다.

    public int maxCount = 30; //추후 슬롯갯수 제한이 필요하면 쓰면된다.
    private int itemCount = 0; //현재 아이템 수

    private List<SaveItemData> testItemList = new List<SaveItemData>(); //아이템 정보 띄울거

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
                var newSlot = Instantiate(prefab, scrollRect.content); //UI객체를 생성할때는 부모 오브젝트 트랜스폼이 중요하다. / 부모 오브젝트 설정
                newSlot.slotIndex = i; //슬롯 번호
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
        var newSlot = Instantiate(prefab, scrollRect.content); //UI객체를 생성할때는 부모 오브젝트 트랜스폼이 중요하다. / 부모 오브젝트 설정
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
