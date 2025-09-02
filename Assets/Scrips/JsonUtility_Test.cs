using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

[Serializable] //NewtonJson은 어트리뷰트로 직렬화를 직접 해줘야 사용이 가능하다.
public class SomeClass
{
    public int number;
}

public class PlayerState
{
    //멤버
    public string playerName;
    public int lives;
    public float health;
    public int[] array;
    public Vector3 v;
    public SomeClass somObj;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {array[0]}";
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

public class JsonUtility_Test : MonoBehaviour
{
    private void Start()
    {
        var obj = new PlayerState
        {
            playerName = "ABC",
            lives = 10,
            health = 10.999f,
            array = new int [] {9, 8, 7, 6}
        };

        //var json = obj.SaveToString();
        //var json = JsonUtility.ToJson(obj); //overwrite는 객체를 하나 가져와서 덮어쓰는것이다.
        //Debug.Log(json);

        //var obj2 = JsonUtility.FromJson<PlayerState>(json);
        //Debug.Log(obj2);

        //보통은 텍스트 파일을 통째로 읽고 쓰는 작업을 추가로 해준다.
        //경로를 지정할때는 기본 경로 말고 특정 경롤ㄹ 사용해야한다. => Application.persistentDataPath
        //이유 : 어떤 플랫폼이든 정상적으로 동작할 수 있도록 하기 때문이다.
        var path = Path.Combine(Application.persistentDataPath, "test.json");
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented, new Vector3Converter()); //객체를 Json 문자열로 반환하는 함수다. / Formattion.Indented : 들여쓰기
        File.WriteAllText(path, json);

        var json2 = File.ReadAllText(path); //파일 경로에서 string을 반환하는 함수
        var obj2 = JsonConvert.DeserializeObject<PlayerState>(json2, new Vector3Converter());
        Debug.Log(obj2);
    }
}
