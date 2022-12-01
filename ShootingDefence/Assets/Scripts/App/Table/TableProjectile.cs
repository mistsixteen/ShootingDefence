using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableProjectileRow
{
    public int TID;
    public string Name;
    public float Speed;
    public float Damage;
    public float PushPower;
    public int LifeSpan;
    public string ProjColor;
    public string TrailColor;
}

public class TableProjectile
{
    public Dictionary<int, TableProjectileRow> TableProjectileRows;
    public TableProjectile()
    {
        TableProjectileRows = new Dictionary<int, TableProjectileRow>();
        List<Dictionary<string, object>> TableData = CSVReader.Read("Tables/Projectiles");
        for (var i = 0; i < TableData.Count; i++)
        {
            int tid = int.Parse(TableData[i]["TID"].ToString());
            if (tid > 0)
            {
                var newTableRow = new TableProjectileRow
                {
                    TID = tid,
                    Name = TableData[i]["Name"].ToString(),
                    Speed = float.Parse(TableData[i]["Speed"].ToString()),
                    Damage = float.Parse(TableData[i]["Damage"].ToString()),
                    PushPower = float.Parse(TableData[i]["PushPower"].ToString()),
                    LifeSpan = int.Parse(TableData[i]["LifeSpan"].ToString()),
                    ProjColor = TableData[i]["ProjColor"].ToString(),
                    TrailColor = TableData[i]["TrailColor"].ToString()
                };

                TableProjectileRows.Add(tid, newTableRow);
            }
        }
    }

    public TableProjectileRow GetTableRow(int tid)
    {
        TableProjectileRow tableRow;
        if (TableProjectileRows.TryGetValue(tid, out tableRow))
        {
            return tableRow;
        }
        else
            return null;
    }
}
