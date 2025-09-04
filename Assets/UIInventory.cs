using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList slotList;

    private void OnEnable()
    {
        OnChangeSorting(sorting.value); //value ; 현재 드롭다운에 활성화 되어있는 값
        OnChangeFiltering(filtering.value);
    }

    public void OnChangeSorting(int index)
    {
        slotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        slotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }
}
