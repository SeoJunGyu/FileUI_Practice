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
    //정렬, 필터링 객체 -> 인터페이스 또는 람다식 등
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
        None, //없음이면 전부 보여주는것이다.
        Weapon,
        Equip,
        Consumale,

    }

    //Comparison : CompareTo<T, T> 델리게이트가 있는 '비교기'다.
    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => lhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
        (lhs, rhs) => lhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
        (lhs, rhs) => lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
        (lhs, rhs) => lhs.itemData.Cost.CompareTo(lhs.itemData.Cost),
    }; //정렬

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        x => true,
        x => x.itemData.Type == ItemTypes.Weapon,
        x => x.itemData.Type == ItemTypes.Equip,
        x => x.itemData.Type == ItemTypes.Consumable,
    }; //필터링

    public InvenSlot prefab;

    public ScrollRect scrollRect;

    private List<InvenSlot> slotList = new List<InvenSlot>(); //시작부터 생성하는 방식이라면 시작하자마자 생성해서 SetEmpty로 할당한다.

    public int maxCount = 30; //추후 슬롯갯수 제한이 필요하면 쓰면된다.
    private int itemCount = 0; //현재 아이템 수

    private List<SaveItemData> testItemList = new List<SaveItemData>(); //아이템 정보 띄울거

    private SortingOptions sorting = SortingOptions.NameAccending; //정렬 비교 객체
    private FilteringOptions filtering = FilteringOptions.None; //필터링 비교 객체
    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            //1. 필터링된 리스트 가져오기 / 2. 정렬 실행 / 3. 슬롯 업데이트
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

    private int selectedSlotIndex = -1; //-1은 선택된게 없는거다.

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    //테스트 세이브 앤 로드
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
        var list = itemList.Where(filterings[(int)filtering]).ToList(); //먼저 필터링 된 리스트를 저장한다.
        list.Sort(comparisons[(int)sorting]); //정렬

        if (slotList.Count < list.Count)
        {
            for (int i = slotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content); //UI객체를 생성할때는 부모 오브젝트 트랜스폼이 중요하다. / 부모 오브젝트 설정
                newSlot.slotIndex = i; //슬롯 번호
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                var button = newSlot.GetComponent<UnityEngine.UI.Button>(); //버튼 가져오기
                button.onClick.AddListener(() => {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.ItemData);
                }); //만들어져있는 슬롯 클릭시 클릭된 버튼의 인덱스로 갱신된다. / 굳이 이렇게 안해도 부모의 레퍼런스를 걸어서 인덱스를 가져올 수도있다.

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

        selectedSlotIndex = -1; //선택된 슬롯 리셋

        onUpdateSlots.Invoke();
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
