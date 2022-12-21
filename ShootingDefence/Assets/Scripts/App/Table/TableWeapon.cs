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
    public int BulletType;
    public int MagazineSize;
    public float ReloadTime;
    public float FireTime;
    public float FireSpread;
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
                var newTableRow = new TableWeaponRow
                {
                    TID = tid,
                    Type = TableData[i]["Type"].ToString(),
                    Name = TableData[i]["Name"].ToString(),
                    Desc = TableData[i]["Desc"].ToString(),
                    ProjTID = int.Parse(TableData[i]["ProjTID"].ToString()),
                    BulletType = int.Parse(TableData[i]["BulletType"].ToString()),
                    MagazineSize = int.Parse(TableData[i]["MagazineSize"].ToString()),
                    ReloadTime = float.Parse(TableData[i]["ReloadTime"].ToString()),
                    FireTime = float.Parse(TableData[i]["FireTime"].ToString()),
                    FireSpread = float.Parse(TableData[i]["FireSpread"].ToString()),
                };

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
