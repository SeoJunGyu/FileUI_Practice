using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveItemData
{
    public Guid instanceId; //���� ���̵� Ŭ������ ����Ҷ� / ��ư ����Ʈ�� �̰� �ڵ����� ���� �������� �Ǵ� ����� �ִ�.

    [JsonConverter(typeof(ItemDataConverter))] //��ưJosn�� ���̽� ������ ��Ʈ����Ʈ��.
    public ItemData itemData; //����ȭ�ϱ� ���ؼ� JsonConveter�� �������Ѵ�.

    public DateTime creationTime; //�ð����� ���̵� ���鶧

    public SaveItemData() 
    {
        instanceId = Guid.NewGuid(); //���ο� ���� ���̵� ��ȯ �Լ�
        creationTime = DateTime.Now; //����ð�
    }
}
