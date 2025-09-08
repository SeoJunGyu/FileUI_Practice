using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public List<GenericWindow> windows; //윈도우를 드래그 드롭으로 연결할 수 있다.

    public Windows defaultWindow; //가장 처음으로 열릴 윈도우다.

    public Windows CurrentWindow { get; private set; }

    private void Start()
    {
        //다른 윈도우 전부 닫고 디폴트 윈도우만 연다.
        foreach(var window in windows)
        {
            window.Init(this);
            //window.Close(); //게임오브젝트 false, 그외로 이런저런 동작을 할 수 있다. (파일 세이브 같은 동작)
            window.gameObject.SetActive(false);
        }

        CurrentWindow = defaultWindow;

        windows[(int)CurrentWindow].Open(); //현재 윈도우 열기
    }

    public void Open(Windows id)
    {
        windows[(int)CurrentWindow].Close(); //현재 열려있는 윈도우 닫기
        CurrentWindow = id;
        windows[(int)CurrentWindow].Open();
    }


}
