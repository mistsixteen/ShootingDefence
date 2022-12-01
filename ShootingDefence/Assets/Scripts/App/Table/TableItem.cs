using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemRow
{
    public int TID;
    public string Type;
    public string Name;
    public string Desc;
}

public class TableItem
{
    public Dictionary<int, TableItemRow> TableItemRows;
    public TableItem()
    {
        TableItemRows = new Dictionary<int, TableItemRow>();
        List<Dictionary<string, object>> TableData = CSVReader.Read("Tables/Items");
        for(var i = 0; i < TableData.Count; i++)
        {
            int tid = int.Parse(TableData[i]["TID"].ToString());
            if(tid > 0)
            {
                var newItemRow = new TableItemRow
                {
                    TID = tid,
                    Type = TableData[i]["Type"].ToString(),
                    Name = TableData[i]["Name"].ToString(),
                    Desc = TableData[i]["Desc"].ToString()
                };

                TableItemRows.Add(tid, newItemRow);
            }   
        }
    }

    public TableItemRow GetTableRow(int tid)
    {
        TableItemRow tableRow;
        if (TableItemRows.TryGetValue(tid, out tableRow))
        {
            return tableRow;
        }
        else
            return null;
    }


}
