using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public bool canContinue = true;

    public Button continueButton;
    public Button newGameButton;
    public Button optionButton;

    protected void Awake()
    {
        //AddListener : 기존에 연결된 메서드들을 유지하고 새로운 이벤트를 추가하는 함수다.
        //continueButton.onClick.AddListener(() => Debug.Log("OnClickContinue")); //람다식도 가능하다.
        continueButton.onClick.AddListener(OnClickContinue);
        newGameButton.onClick.AddListener(OnClickNewGame);
        optionButton.onClick.AddListener(OnClickOption);
    }

    public override void Open()
    {
        //Open은 포커스된 윈도우가 열리기때문에 부모 전에 작업을 해줘야한다.
        continueButton.gameObject.SetActive(canContinue);
        firstSelected = continueButton.gameObject.activeSelf ? continueButton.gameObject : newGameButton.gameObject;

        base.Open();
    }

    public void OnClickContinue()
    {
        Debug.Log("OnClickContinue");
    }

    public void OnClickNewGame()
    {
        manager.Open(Windows.GameOver);
    }

    public void OnClickOption()
    {
        Debug.Log("OnClickOption");
    }
}
