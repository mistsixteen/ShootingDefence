using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWeapon
{
    public List<Dictionary<string, object>> TableData;
    public TableWeapon()
    {
        TableData = CSVReader.Read("Tables/Weapons");
        for(var i = 0; i < TableData.Count; i++)
        {
            Debug.Log("ItemCode : " + TableData[i]["TID"]);
        }
    }
}
