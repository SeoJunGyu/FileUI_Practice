using UnityEngine;
using System;

public class SaveItemData
{
    public Guid instanceId; //고유 아이디 클래스를 사용할때 / 뉴튼 소프트는 이게 자동으로 직렬 역직렬이 되는 기능이 있다.

    public ItemData itemData;

    public DateTime creationTime; //시간으로 아이디를 만들때

    public SaveItemData() 
    {
        instanceId = Guid.NewGuid(); //새로운 고유 아이디 반환 함수
        creationTime = DateTime.Now; //현재시간
    }
}
