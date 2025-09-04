using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ItemDataConverter : JsonConverter<ItemData>
{
    public override ItemData ReadJson(JsonReader reader, Type objectType, ItemData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var id = reader.Value as string; //밑의 쓰기와 반대되는 방식이다. 아래는 메서드 읽쓰 방식, 여기는? -> 이거는 별로 좋은 방식은 아니다.
        return DataTableManager.ItemTable.Get(id);
    }

    public override void WriteJson(JsonWriter writer, ItemData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id); //
    }
}

public class Vector3Converter : JsonConverter<Vector3>
{
    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Vector3 v = Vector3.zero;
        JObject jobj = JObject.Load(reader); //JObject 객체 생성

        //""는 내가 작성하는 이름
        v.x = (float)jobj["X"]; //Jobject 형식으로 넘어오기에 알맞은 데이터형으로 형변환 해줘야한다.
        v.y = (float)jobj["Y"]; 
        v.z = (float)jobj["Z"]; 

        return v;
    }

    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject(); //중괄효 열기

        //키 이름을 지정하고, 벨류를 할당해야한다.
        writer.WritePropertyName("X");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);

        writer.WriteEndObject(); //중괄효 닫기
    }
}

public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Quaternion q = Quaternion.identity;
        JObject jobj = JObject.Load(reader); //JObject 객체 생성

        //""는 내가 작성하는 이름
        q.x = (float)jobj["X"]; //Jobject 형식으로 넘어오기에 알맞은 데이터형으로 형변환 해줘야한다.
        q.y = (float)jobj["Y"];
        q.z = (float)jobj["Z"];

        return q;
    }

    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        //키 이름을 지정하고, 벨류를 할당해야한다.
        writer.WritePropertyName("X");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);

        writer.WriteEndObject();
    }
}

public class ColorConverter : JsonConverter<Color>
{
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Color color = Color.black;
        JObject jobj = JObject.Load(reader); //JObject 객체 생성

        //""는 내가 작성하는 이름
        color.r = (float)jobj["R"]; //Jobject 형식으로 넘어오기에 알맞은 데이터형으로 형변환 해줘야한다.
        color.g = (float)jobj["G"];
        color.b = (float)jobj["B"];
        color.a = (float)jobj["A"];

        return color;
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        //키 이름을 지정하고, 벨류를 할당해야한다.
        writer.WritePropertyName("R");
        writer.WriteValue(value.r);
        writer.WritePropertyName("G");
        writer.WriteValue(value.g);
        writer.WritePropertyName("B");
        writer.WriteValue(value.b);
        writer.WritePropertyName("A");
        writer.WriteValue(value.a);

        writer.WriteEndObject();
    }
}

public class GameObjectConverter : JsonConverter<GameObject>
{
    public override GameObject ReadJson(JsonReader reader, Type objectType, GameObject existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        GameObject obj = new GameObject();

        JObject jobj = JObject.Load(reader);

        float x = (float)jobj["position"]["X"];
        float y = (float)jobj["position"]["Y"];
        float z = (float)jobj["position"]["Z"];
        Debug.Log($"{x} / {y} / {z}");

        obj.transform.position = new Vector3(x, y, z);

        x = (float)jobj["rotation"]["X"];
        y = (float)jobj["rotation"]["Y"];
        z = (float)jobj["rotation"]["Z"];

        obj.transform.rotation = Quaternion.Euler(x, y, z);

        x = (float)jobj["rotation"]["X"];
        y = (float)jobj["rotation"]["Y"];
        z = (float)jobj["rotation"]["Z"];

        obj.transform.localScale = new Vector3(x, y, z);

        return obj;
    }

    public override void WriteJson(JsonWriter writer, GameObject value, JsonSerializer serializer)
    {
        Renderer cubeColor = value.GetComponent<Renderer>();

        writer.WriteStartObject();

        //키 이름을 지정하고, 벨류를 할당해야한다.
        writer.WritePropertyName("position");
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(value.transform.position.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.transform.position.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.transform.position.z);
        writer.WriteEndObject();

        writer.WritePropertyName("rotation");
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(value.transform.rotation.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.transform.rotation.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.transform.rotation.z);
        writer.WriteEndObject();

        writer.WritePropertyName("scale");
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(value.transform.localScale.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.transform.localScale.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.transform.localScale.z);
        writer.WriteEndObject();

        writer.WritePropertyName("color");
        writer.WriteStartObject();
        writer.WritePropertyName("r");
        writer.WriteValue(cubeColor.material.color.r);
        writer.WritePropertyName("g");
        writer.WriteValue(cubeColor.material.color.g);
        writer.WritePropertyName("b");
        writer.WriteValue(cubeColor.material.color.b);
        writer.WriteEndObject();

        writer.WriteEndObject();
    }
}
