using System.Collections;
using System.Collections.Generic;

public class ModelManager
{
    public ModelUser ModelUser;
    public ModelInventory ModelInventory;

    public ModelManager()
    {
        ModelUser = new ModelUser();
        ModelInventory = new ModelInventory();
    }
}
