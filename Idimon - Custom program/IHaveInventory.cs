using System;
using SplashKitSDK;

namespace Idimon
{
    public interface IHaveInventory
    {
        string Name { get; }
        Inventory Inventory { get; }
    }
}
