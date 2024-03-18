using Godot;
using System;

public partial class PauseMenu : Control
{
	public static PauseMenu Singleton;

	BaseButton resumeButton;
	BaseButton settingsButton;
	BaseButton exitButton;

    public override void _Ready()
    {
        Hide();
        Singleton = this;
        resumeButton = GetNode<BaseButton>("Panel/VBoxContainer/Label/Resume");
		resumeButton.Pressed += OnResumePressed;
        settingsButton = GetNode<BaseButton>("Panel/VBoxContainer/Label/Settings");
        settingsButton.Pressed += OnSettingsPressed;
        exitButton = GetNode<BaseButton>("Panel/VBoxContainer/Label/Exit");
        exitButton.Pressed += OnExitPressed;
    }
	public void TogglePauseMenu()
	{
		Visible = !Visible;
	}
	private void OnResumePressed()
	{
		TogglePauseMenu();
	}
	private void OnSettingsPressed()
	{
		
	}
	private void OnExitPressed()
	{
		GetTree().Quit();
    }
}
