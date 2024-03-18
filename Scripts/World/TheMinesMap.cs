using Godot;
using System;

public partial class TheMinesMap : TileMap
{
    public static TheMinesMap Singleton;

    public override void _Ready()
	{
		Singleton = this;
	}
	
	public override void _Process(double delta)
	{

	}
}
