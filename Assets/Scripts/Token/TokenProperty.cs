using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トークン共通のパラメータ関連
/// </summary>
public class TokenProperty : MonoBehaviour
{
    /// <summary>
    /// 横幅
    /// </summary>
    private float _width = 0.0f;
    public float Width
    {
        get { return _width; }
        set { _width = value; }
    }

    /// <summary>
    /// 高さ
    /// </summary>
    private float _height = 0.0f;
    public float Height
    {
        get { return _height; }
        set { _height = value; }
    }

    /// <summary>
    /// 移動速度
    /// </summary>
    private float _moveSpeed = 5.0f;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    /// <summary>
    /// 存在フラグ
    /// </summary>
    private bool _exists = true;
    public bool Exists
    {
        get { return _exists; }
        set { _exists = value; }
    }

    /// <summary>
    /// The renderer.
    /// </summary>
    private SpriteRenderer _renderer = null;
    public SpriteRenderer Renderer
    {
        get { return _renderer ?? (_renderer = gameObject.GetComponent<SpriteRenderer>()); }
    }

    /// <summary>
    /// The rigidbody2 d.
    /// </summary>
    private Rigidbody2D _rigidbody2D = null;
    public Rigidbody2D RigidBody
    {
        get { return _rigidbody2D ?? (_rigidbody2D = gameObject.GetComponent<Rigidbody2D>()); }
    }

    /// <summary>
    /// The circle collider.
    /// </summary>
    private CircleCollider2D _circleCollider = null;
    public CircleCollider2D CircleCollider
    {
        get { return _circleCollider ?? (_circleCollider = GetComponent<CircleCollider2D>()); }
    }

    /// <summary>
    /// The box collider.
    /// </summary>
    private BoxCollider2D _boxCollider = null;
    public BoxCollider2D BoxCollider
    {
        get { return _boxCollider ?? (_boxCollider = GetComponent<BoxCollider2D>()); }
    }
}
