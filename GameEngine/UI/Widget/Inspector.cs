﻿using System.Numerics;
using System.Reflection;
using Quaternion = OpenTK.Mathematics.Quaternion;
using ImGuiNET;

public class Inspector
{
    private GameObjectData? _gameObjectToBeShown;
    private readonly float _draggingSpeed = 0.05f;

    public void InspectGameObject(GameObjectData data)
    {
        _gameObjectToBeShown = data;
    }
    
    public void DrawInspector(Window window, float width)
    {
        ImGui.Begin("Inspector", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize);
        ImGui.SetWindowPos(new Vector2(window.Width - width, 0));
        ImGui.SetWindowSize(new Vector2(width, window.Height));

        if (_gameObjectToBeShown != null)
        {
            MapTransform();
            DrawComponents();
        }

        ImGui.End();
    }

    private void MapTransform()
    {
        Vector3 position = _gameObjectToBeShown!.Transform.Position.ToNumeric();
        Vector3 rotation = _gameObjectToBeShown.Transform.Rotation.ToEulerAngles().ToNumeric();
        Vector3 scale = _gameObjectToBeShown.Transform.Scale.ToNumeric();
        
        ImGui.PushItemWidth(300);
        ImGui.DragFloat3("Position", ref position, _draggingSpeed);
        ImGui.DragFloat3("Rotation", ref rotation, _draggingSpeed);
        ImGui.DragFloat3("Scale", ref scale, _draggingSpeed);

        _gameObjectToBeShown.Transform.Position = position.ToOpenTk();
        _gameObjectToBeShown.Transform.Rotation = Quaternion.FromEulerAngles(rotation.ToOpenTk());
        _gameObjectToBeShown.Transform.Scale = scale.ToOpenTk();
    }

    private void DrawComponents()
    {
        foreach (IUpdatable component in _gameObjectToBeShown!.Components)
        {
            ShowHeader(component);
            ShowProperties(component);
            ImGui.Spacing();
        }
    }

    private void ShowHeader(IUpdatable component)
    {
        string name = component.GetType().Name;
        
        if (component is GameComponent gameComponent)
        {
            ImGui.Checkbox(name, ref gameComponent.Enabled);
        }
        else
        {
            ImGui.Text(name);
        }
    }

    private void ShowProperties(IUpdatable component)
    {
        FieldInfo[] fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.IsDefined(typeof(EditorField), false))
            {
                float value = (float)fieldInfo.GetValue(component)!;
                
                ImGui.PushItemWidth(100);
                ImGui.DragFloat(fieldInfo.Name, ref value, _draggingSpeed);
                
                fieldInfo.SetValue(component, value);
            }
        }
    }
}