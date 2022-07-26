﻿using OpenTK.Mathematics;

public class SphereCollider : ICollider
{
    public readonly float Radius;
    private readonly Transform _transform;
    private readonly Vector4 _centreOfSphere = new(0, 0, 0, 1);
    
    public SphereCollider(Transform transform, float radius)
    {
        _transform = transform;
        Radius = radius;
    }

    public Vector3 Centre => (_centreOfSphere * _transform.ModelMatrix).Xyz;
    
    public bool CheckCollision(ICollider collider) => collider.CheckCollision(this);

    public bool CheckCollision(BoxCollider boxCollider) => CollisionDetection.CheckCollision(boxCollider, this);
    
    public bool CheckCollision(MeshCollider meshCollider) => false;

    public bool CheckCollision(SphereCollider sphereCollider) => CollisionDetection.CheckCollision(this, sphereCollider);
}