using UnityEngine;
using UnityEngine.EventSystems;

//�����츦 ���� ������ �۾��ؾ��� ��� ���� Ŭ������.
public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;

    protected WindowManager manager;

    public void Init(WindowManager mgr)
    {
        manager = mgr;
    }

    public void OnFocus()
    {
        //Camera.main : ���� �����ϴ� ���� ���� ī�޶� ������Ʈ�� �������� ������Ƽ��. 
        //���� �����ϴ� ������ �̺�Ʈ �ý��� ������Ʈ�� ���� �۵����� �̺�Ʈ(���콺 �̵�, Ŭ�� ��)�� �������� ������Ƽ��.
        EventSystem.current.SetSelectedGameObject(firstSelected); //SetSelectedGameObject : ������Ʈ�� �ѱ�� �ش� ������Ʈ�� ���� ������Ʈ���� ���� ���·� ����� �Լ���.
    }

    //���������� ����
    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnFocus();
    }

    //������ ����
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
