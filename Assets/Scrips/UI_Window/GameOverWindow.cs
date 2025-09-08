using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI textLeftStat;
    public TextMeshProUGUI textLeftNumber;
    public TextMeshProUGUI textRightStat;
    public TextMeshProUGUI textRightNumber;
    public TextMeshProUGUI textTotalScore;

    private int count = 0;
    private int MaxCount = 7;

    private Coroutine coroutine;

    protected void Awake()
    {
        
    }

    public override void Open()
    {
        textLeftStat.text = "";
        textLeftNumber.text = "";
        textRightStat.text = "";
        textRightNumber.text = "";
        textTotalScore.text = "";

        base.Open();
        
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(CoGameOverLeft());
    }

    public IEnumerator CoGameOverLeft()
    {
        for(int i = 0; i < MaxCount; i++)
        {
            if (count < 3)
            {
                textLeftStat.text += "STAT " + count++.ToString("D2") + "\n";
                textLeftNumber.text += Random.Range(10, 99) + "\n";
            }
            else if (count < 6) 
            {
                textRightStat.text += "STAT " + count++.ToString("D2") + "\n";
                textRightNumber.text += Random.Range(10, 99) + "\n";
            }
            else
            {
                textTotalScore.text = Random.Range(100000, 99999999).ToString("D8");
            }

            yield return new WaitForSeconds(0.5f);
        }

        coroutine = null;
    }
}
