using Godot;

public partial class ItemStackDropManager : Node
{
    public static ItemStackDropManager Singleton;
    private PackedScene ItemStackDropScene = GD.Load<PackedScene>("res://Scenes/Player/itemStackDrop.tscn");

    public override void _Ready()
    {
        Singleton = this;
    }

    public static ItemStackDrop CreateDrop(ItemStack itemStack)
    {
        ItemStackDrop drop = Singleton.ItemStackDropScene.Instantiate<ItemStackDrop>();
        Singleton.AddChild(drop);
        drop.Init(itemStack);
        return drop;
    }
}
