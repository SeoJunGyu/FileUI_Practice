using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public List<GenericWindow> windows; //�����츦 �巡�� ������� ������ �� �ִ�.

    public Windows defaultWindow; //���� ó������ ���� �������.

    public Windows CurrentWindow { get; private set; }

    private void Start()
    {
        //�ٸ� ������ ���� �ݰ� ����Ʈ �����츸 ����.
        foreach(var window in windows)
        {
            window.Init(this);
            //window.Close(); //���ӿ�����Ʈ false, �׿ܷ� �̷����� ������ �� �� �ִ�. (���� ���̺� ���� ����)
            window.gameObject.SetActive(false);
        }

        CurrentWindow = defaultWindow;

        windows[(int)CurrentWindow].Open(); //���� ������ ����
    }

    public void Open(Windows id)
    {
        windows[(int)CurrentWindow].Close(); //���� �����ִ� ������ �ݱ�
        CurrentWindow = id;
        windows[(int)CurrentWindow].Open();
    }


}
