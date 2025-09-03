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
        //Resources 폴더에 있는 에셋을 가져오는 코드
        TextAsset csv = Resources.Load<TextAsset>("DataTables/StringTable");
        //Resources.UnloadAsset(csv); //한번 로드된 에셋은 메모리에서 내려오지 않는다. / 즉, 언로드를 하지 않으면 계속 메모리에 남아있게 된다.

        //CultureInfo.InvariantCulture : 지역설정 무관하게 처리하는 옵션이다.
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
