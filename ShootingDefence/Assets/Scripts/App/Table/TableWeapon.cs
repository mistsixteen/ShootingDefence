using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWeaponRow
{
    public int TID;
    public string Type;
    public string Name;
    public string Desc;
    public int ProjTID;
}

public class TableWeapon
{
    public Dictionary<int, TableWeaponRow> TableWeaponRows;
    public TableWeapon()
    {
        TableWeaponRows = new Dictionary<int, TableWeaponRow>();
        List<Dictionary<string, object>> TableData = CSVReader.Read("Tables/Weapons");
        for (var i = 0; i < TableData.Count; i++)
        {
            int tid = int.Parse(TableData[i]["TID"].ToString());
            if (tid > 0)
            {
                var newTableRow = new TableWeaponRow();
                newTableRow.TID = tid;
                newTableRow.Type = TableData[i]["Type"].ToString();
                newTableRow.Name = TableData[i]["Name"].ToString();
                newTableRow.Desc = TableData[i]["Desc"].ToString();
                newTableRow.ProjTID = int.Parse(TableData[i]["ProjTID"].ToString());

                TableWeaponRows.Add(tid, newTableRow);
            }
        }
    }

    public TableWeaponRow GetTableRow(int tid)
    {
        TableWeaponRow tableRow;
        if (TableWeaponRows.TryGetValue(tid, out tableRow))
        {
            return tableRow;
        }
        else
            return null;
    }


}
