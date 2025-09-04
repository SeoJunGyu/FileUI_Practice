using UnityEngine;
using System;

public class SaveItemData
{
    public Guid instanceId; //���� ���̵� Ŭ������ ����Ҷ� / ��ư ����Ʈ�� �̰� �ڵ����� ���� �������� �Ǵ� ����� �ִ�.

    public ItemData itemData;

    public DateTime creationTime; //�ð����� ���̵� ���鶧

    public SaveItemData() 
    {
        instanceId = Guid.NewGuid(); //���ο� ���� ���̵� ��ȯ �Լ�
        creationTime = DateTime.Now; //����ð�
    }
}
