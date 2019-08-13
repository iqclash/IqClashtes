﻿
using System;
using UnityEngine;

public class Platform : MonoBehaviour , IPoolable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Transform _transform;

    public Transform Transform => _transform;

    public Vector2 Size => _spriteRenderer.bounds.size;
        
    private void Awake()
    {
        _transform = transform;
    }

    public void OnReturnToPool()
    {
    }

    public void OnGetInPool()
    {
    }
}
