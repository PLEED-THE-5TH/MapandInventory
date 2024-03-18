using Godot;
using System;

public partial class ExternalInventory : Control
{
    public static ExternalInventory Singleton;

    public override void _Ready()
	{
		Hide();
		Singleton = this;
	}

	private void ToggleExternalInventory()
	{
		Visible = !Visible;
	}
}
