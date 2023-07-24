using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new tool class", menuName = "item/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tool")]
    public ToolType tooltype;
    public enum ToolType
    {
        Weapon,
        Lighter,
    }
    public override ItemClass GetItem()
    {
        return this;
    }
    public override ToolClass GetTool()
    {
        return this;
    }
    public override MiscClass GetMisc()
    {
        return null;
    }
    public override ConsumableClass GetConsumable()
    {
        return null;
    }
}
