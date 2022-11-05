﻿using OpenTK.Mathematics;

public class RotationAnimation : IUpdatable
{
    private readonly Transform _transform;
    private readonly Vector3 _rotationVector;
    private readonly float _rotationSpeed;

    public RotationAnimation(Transform transform, Vector3 rotationVector, float rotationSpeed)
    {
        _transform = transform;
        _rotationVector = rotationVector;
        _rotationSpeed = rotationSpeed;
    }

    public void Update(float deltaTime)
    {
        _transform.Rotate(_rotationVector * deltaTime * _rotationSpeed);
    }
}