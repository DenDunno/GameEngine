﻿using System.Numerics;
using Dear_ImGui_Sample;
using ImGuiNET;
using OpenTK.Windowing.Common;

public class Editor : IEngineComponent
{
    private readonly ImGuiController _imGui;
    private readonly PlayModeSwitching _playModeSwitching;
    private readonly UI _ui;
    
    public Editor(Window window, World world)
    {
        _ui = new UI(window, world);
        _imGui = new ImGuiController(window);
        _playModeSwitching = new PlayModeSwitching(window, world);
    }

    public void Initialize()
    {
        _ui.InitStyle();
    }

    public void Update(FrameEventArgs args)
    {
        float deltaTime = (float) args.Time;
        
        _imGui.Update(deltaTime);
        _playModeSwitching.Update(deltaTime);
        _ui.Update(deltaTime);
    }

    public void Draw(FrameEventArgs obj)
    {
        if (_playModeSwitching.IsEditorMode)
        {
            ImGui.Begin("Main", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoCollapse |
                                ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | 
                                ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoBringToFrontOnFocus | 
                                ImGuiWindowFlags.NoNavFocus | ImGuiWindowFlags.NoDocking);
            
            ImGui.SetWindowPos(Vector2.Zero);
            ImGui.SetWindowSize(ImGui.GetIO().DisplaySize);
            ImGui.DockSpace(ImGui.GetID("Main"));
            ImGui.End();
            _ui.DrawMain();
            _imGui.Render();
        }
    }

    public void OnMouseWheel(MouseWheelEventArgs args)
    {
        _imGui.MouseScroll(args.Offset);
    }

    public void OnWindowResize(ResizeEventArgs args)
    {
        _imGui.WindowResized(args.Width, args.Height);
    }
}