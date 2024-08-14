using System;

namespace SwinAdventure
{
    public interface IHaveInventory
    {
        GameObject Locate(string id);
        //Item Take(string id);
        string Name { get; }
        Inventory Inventory { get; }
    }
}
