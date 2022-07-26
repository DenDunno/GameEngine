﻿using OpenTK.Graphics.OpenGL;

public class RenderData
{
    public Transform Transform { get; init; } = null!;
    public Mesh Mesh { get; init; } = null!;
    public Material Material { get; init; } = null!;
    public BufferUsageHint BufferUsageHint { get; init; }
}