using Godot;

public partial class ItemStackDrop : CharacterBody2D
{
    Sprite2D sprite;
    Timer timer;
    Area2D area;
    ItemStack itemStack;

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        timer = GetNode<Timer>("Timer");
        area = GetNode<Area2D>("Pickup Area");
    }
    public void Init(ItemStack initItemStack)
    {
        itemStack = initItemStack;
        sprite.Texture = itemStack.item.icon;
        Position = Player.Singleton.Position;
        itemStack.Changed += (s, e) => { OnItemStackChanged(); };
    }
    public override void _Process(double delta)
    {
        CheckPickup();
        Rotate((float)delta);
        MoveAndSlide();
    }
    public void CheckPickup()
    {
        if (timer.TimeLeft > 0) {
            return;
        }
    }
    public void OnItemStackChanged()
    {
        if (itemStack.IsEmpty()) {
            QueueFree();
        }
        if (!area.OverlapsBody(Player.Singleton)) {
            return;
        }
        Player.Singleton.Inventory.AutoAdd(itemStack);  // I need to add the player scripts and the inventory scripts for this to work, i dont have the functions
    }
}