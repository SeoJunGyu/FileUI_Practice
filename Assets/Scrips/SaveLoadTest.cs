using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManage.Data = new SaveDataV1();
            SaveLoadManage.Data.PlayerName = "TEST";

            SaveLoadManage.Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SaveLoadManage.Load();

            Debug.Log(SaveLoadManage.Data.PlayerName);
        }
    }
}
