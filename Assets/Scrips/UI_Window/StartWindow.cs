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
        //AddListener : ������ ����� �޼������ �����ϰ� ���ο� �̺�Ʈ�� �߰��ϴ� �Լ���.
        //continueButton.onClick.AddListener(() => Debug.Log("OnClickContinue")); //���ٽĵ� �����ϴ�.
        continueButton.onClick.AddListener(OnClickContinue);
        newGameButton.onClick.AddListener(OnClickNewGame);
        optionButton.onClick.AddListener(OnClickOption);
    }

    public override void Open()
    {
        //Open�� ��Ŀ���� �����찡 �����⶧���� �θ� ���� �۾��� ������Ѵ�.
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
