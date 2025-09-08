using UnityEngine;
using UnityEngine.EventSystems;

//윈도우를 열고 닫을때 작업해야할 기능 정의 클래스다.
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
        //Camera.main : 지금 실행하는 씬의 메인 카메라 컴포넌트를 가져오는 프로퍼티다. 
        //현재 동작하는 씬에서 이벤트 시스템 컴포넌트에 현재 작동중인 이벤트(마우스 이동, 클릭 등)를 가져오는 프로퍼티다.
        EventSystem.current.SetSelectedGameObject(firstSelected); //SetSelectedGameObject : 오브젝트를 넘기면 해당 오브젝트에 붙은 컴포넌트들을 눌린 상태로 만드는 함수다.
    }

    //열려있을때 동작
    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnFocus();
    }

    //닫힐때 동작
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
