using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public static Player Singleton;
    private float walkingSpeed = 400;

    public override void _Ready()
	{
		Singleton = this;
        inventory = Inventory.new(12 * 8, 12, self);
        PlayerInventoryUI.Singleton.Init(inventory);
        //var apple: Apple = Apple.new()
        //var book: Book = Book.new()
        //var wood_sword: WoodSword = WoodSword.new()
        //inventory.auto_add(ItemStack.new(apple))
        //inventory.auto_add(ItemStack.new(apple, 3))
        //inventory.auto_add(ItemStack.new(book))
        //inventory.auto_add(ItemStack.new(book, 3))
        //inventory.auto_add(ItemStack.new(wood_sword))
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Vector2.Zero;
        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Vector2 direction = new Vector2(inputDir.X, inputDir.Y).Normalized();
        Velocity = direction * walkingSpeed;
        MoveAndSlide();
    }

    public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("esq"))
        {
            PauseMenu.Singleton.TogglePauseMenu();
        }
        if (Input.IsActionJustPressed("inv"))
        {
            PlayerInventory.Singleton.TogglePlayerInventory();
        }
    }
}
