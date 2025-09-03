using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;

public class  CsvData
{
    public string Id { get; set; }
    public string String {  get; set; }
}

public class CSVTest : MonoBehaviour
{

    private void Start()
    {
        //Resources ������ �ִ� ������ �������� �ڵ�
        TextAsset csv = Resources.Load<TextAsset>("DataTables/StringTable");
        //Resources.UnloadAsset(csv); //�ѹ� �ε�� ������ �޸𸮿��� �������� �ʴ´�. / ��, ��ε带 ���� ������ ��� �޸𸮿� �����ְ� �ȴ�.

        //CultureInfo.InvariantCulture : �������� �����ϰ� ó���ϴ� �ɼ��̴�.
        using (var reader = new StringReader(csv.text))
        using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<CsvData>();
            foreach(var record in records)
            {
                Debug.Log($"{record.Id} : {record.String}");
            }
        }

        //Debug.Log(csv.text);
    }
}
