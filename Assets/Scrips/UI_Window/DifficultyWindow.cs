using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    private int index;

    public Button btnBack;

    public ToggleGroup toggleGroup;
    public Toggle[] toggles;

    private void Awake()
    {
        btnBack.onClick.AddListener(OnClickBack);
    }

    private void OnDisable()
    {
        //SaveLoadManager.Data.Name = "TEST";
        //SaveLoadManager.Data.Index = index;
        //SaveLoadManager.Save();
    }

    public override void Open()
    {
        index = SaveLoadManager.Data.Index;

        toggles[index].isOn = true;

        base.Open();
    }

    public override void Close()
    {
        SaveLoadManager.Data.Name = "TEST";
        SaveLoadManager.Data.Index = index;
        SaveLoadManager.Save();

        base.Close();
    }

    public void OnToggle()
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                Debug.Log(i);
                break;
            }
        }
    }

    public void OnClickEasy(bool value)
    {
        if (value)
        {
            Debug.Log("Easy");
            index = 0;
        }
        
    }

    public void OnClickNormal(bool value)
    {
        if (value)
        {
            Debug.Log("Normal");
            index = 1;
        }
    }

    public void OnClickHard(bool value)
    {
        if (value)
        {
            Debug.Log("Hard");
            index = 2; 
        }
            
    }

    public void OnClickBack()
    {
        manager.Open(Windows.Start);
    }
}
