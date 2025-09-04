using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveItemData
{
    public Guid instanceId; //고유 아이디 클래스를 사용할때 / 뉴튼 소프트는 이게 자동으로 직렬 역직렬이 되는 기능이 있다.

    [JsonConverter(typeof(ItemDataConverter))] //뉴튼Josn의 제이슨 컨버터 어트리뷰트다.
    public ItemData itemData; //직렬화하기 위해서 JsonConveter를 만들어야한다.

    public DateTime creationTime; //시간으로 아이디를 만들때

    public SaveItemData() 
    {
        instanceId = Guid.NewGuid(); //새로운 고유 아이디 반환 함수
        creationTime = DateTime.Now; //현재시간
    }
}
