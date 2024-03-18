using Godot;
using System;

public partial class PlayerInventory : Control
{
    public static PlayerInventory Singleton;

    public override void _Ready()
	{
		Hide();
		Singleton = this;
	}

	public void TogglePlayerInventory()
	{
		Visible = !Visible;
	}
}
