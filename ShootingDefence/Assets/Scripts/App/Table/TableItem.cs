using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItem
{
    public List<Dictionary<string, object>> TableData;
    public TableItem()
    {
        TableData = CSVReader.Read("Tables/Items");
        for(var i = 0; i < TableData.Count; i++)
        {
            Debug.Log("ItemCode : " + TableData[i]["TID"]);
        }
    }
}
