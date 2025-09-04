using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiInvenSlotList : MonoBehaviour
{
    //����, ���͸� ��ü -> �������̽� �Ǵ� ���ٽ� ��
    public enum SortingOptions
    {
        CreationTimeAccending,
        CreationTimeDeccending,
        NameAccending,
        NameDeccending,
        CostAccending,
        CoseDeccending,
    }

    public enum FilteringOptions
    {
        None, //�����̸� ���� �����ִ°��̴�.
        Weapon,
        Equip,
        Consumale,

    }

    //Comparison : CompareTo<T, T> ��������Ʈ�� �ִ� '�񱳱�'��.
    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => lhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
        (lhs, rhs) => lhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
        (lhs, rhs) => lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
        (lhs, rhs) => lhs.itemData.Cost.CompareTo(lhs.itemData.Cost),
    }; //����

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        x => true,
        x => x.itemData.Type == ItemTypes.Weapon,
        x => x.itemData.Type == ItemTypes.Equip,
        x => x.itemData.Type == ItemTypes.Consumable,
    }; //���͸�

    public InvenSlot prefab;

    public ScrollRect scrollRect;

    private List<InvenSlot> slotList = new List<InvenSlot>(); //���ۺ��� �����ϴ� ����̶�� �������ڸ��� �����ؼ� SetEmpty�� �Ҵ��Ѵ�.

    public int maxCount = 30; //���� ���԰��� ������ �ʿ��ϸ� ����ȴ�.
    private int itemCount = 0; //���� ������ ��

    private List<SaveItemData> testItemList = new List<SaveItemData>(); //������ ���� ����

    private SortingOptions sorting = SortingOptions.NameAccending; //���� �� ��ü
    private FilteringOptions filtering = FilteringOptions.None; //���͸� �� ��ü
    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            //1. ���͸��� ����Ʈ �������� / 2. ���� ���� / 3. ���� ������Ʈ
            sorting = value;
            UpdateSlots(testItemList);
        }
    }
    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            filtering = value;
            UpdateSlots(testItemList);
        }
    }

    private int selectedSlotIndex = -1; //-1�� ���õȰ� ���°Ŵ�.

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    //�׽�Ʈ ���̺� �� �ε�
    public void Save()
    {
        var jsontext = JsonConvert.SerializeObject(testItemList);
        var filePath = Path.Combine(Application.persistentDataPath, "test.json");
        File.WriteAllText(filePath, jsontext);
    }
    public void Load()
    {
        var filePath = Path.Combine(Application.persistentDataPath, "test.json");
        if (!File.Exists(filePath))
        {
            return;
        }

        var jsonText = File.ReadAllText(filePath);
        testItemList = JsonConvert.DeserializeObject<List<SaveItemData>>(jsonText);

        UpdateSlots(testItemList);
    }

    private void Awake()
    {
        /*
        for (int i = 0; i < maxCount; ++i)
        {
            var newSlot = Instantiate(prefab, scrollRect.content);
            newSlot.slotIndex = i;
            newSlot.SetEmpty();
            newSlot.gameObject.SetActive(false);
            slotList.Add(newSlot);
        }
        */
    }

    private void OnEnable()
    {
        Load();
    }

    private void OnDisable()
    {
        Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Sorting = (SortingOptions)Random.Range(0, 6);
            Debug.Log(Sorting);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Filtering = (FilteringOptions)Random.Range(0, 4);
            Debug.Log(Filtering);
        }
    }

    private void UpdateSlots(List<SaveItemData> itemList)
    {
        var list = itemList.Where(filterings[(int)filtering]).ToList(); //���� ���͸� �� ����Ʈ�� �����Ѵ�.
        list.Sort(comparisons[(int)sorting]); //����

        if (slotList.Count < list.Count)
        {
            for (int i = slotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content); //UI��ü�� �����Ҷ��� �θ� ������Ʈ Ʈ�������� �߿��ϴ�. / �θ� ������Ʈ ����
                newSlot.slotIndex = i; //���� ��ȣ
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                var button = newSlot.GetComponent<UnityEngine.UI.Button>(); //��ư ��������
                button.onClick.AddListener(() => {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.ItemData);
                }); //��������ִ� ���� Ŭ���� Ŭ���� ��ư�� �ε����� ���ŵȴ�. / ���� �̷��� ���ص� �θ��� ���۷����� �ɾ �ε����� ������ �����ִ�.

                slotList.Add(newSlot);
            }
        }

        for (int i = 0; i < slotList.Count; ++i)
        {
            if (i < list.Count)
            {
                slotList[i].gameObject.SetActive(true);
                slotList[i].SetItem(list[i]);
            }
            else
            {
                slotList[i].SetEmpty();
                slotList[i].gameObject.SetActive(false);
            }
        }

        selectedSlotIndex = -1; //���õ� ���� ����

        onUpdateSlots.Invoke();
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

    public void RemoveItem()
    {
        if(selectedSlotIndex == -1)
        {
            return;
        }

        testItemList.Remove(slotList[selectedSlotIndex].ItemData);
        UpdateSlots(testItemList);
    }
}
