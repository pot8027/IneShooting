using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// トークン共通操作関連
/// </summary>
public class TokenController : TokenProperty
{
    /// <summary>
    /// Sets the stagee data.
    /// </summary>
    /// <param name="data">Data.</param>
    public void SetStageeData(StageData data)
    {
        HP = data.Hp;
        MaxHP = data.Hp;
        IsDispHpBar = data.IsDispHP;
        Score = data.Score;
        HasGameClearFlg = data.FlgGameClear;
        FrameJump = data.JumpFrame;
        GeneratePrefubName = data.GeneratePrefubName;

        // HPバー表示
        if (IsDispHpBar)
        {
            HPBarMax.Show();
            HPBarMax.SetMaxHP(HP);
            HPBarMax.SetSpriteX(Renderer.bounds.size.x);
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected void Update()
    {
        if (GameManager.IsPause)
        {
            return;
        }

        UpdateEach();
    }

    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected virtual void UpdateEach()
    {

    }

    /// <summary>
    /// コルーチン制御用IDを変更する
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="forceUpdate">true=IDが同じでも再設定する</param>
    protected void SetCoroutinueID(int id, bool forceUpdate = false)
    {
        // 現在のコルーチンと同じなら何もしない
        if (CurrentCoroutineID == id)
        {
            if (forceUpdate == false)
            {
                return;
            }
        }

        StopCoroutine("IEUpdate" + CurrentCoroutineID);
        CurrentCoroutineID = id;
        StartCoroutine("IEUpdate" + CurrentCoroutineID);
    }

    /// <summary>
    /// スコアテキスト
    /// </summary>
    public static Score _scoreText = null;
    public static void AddScore(float score)
    {
        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("Score").GetComponent<Score>();
        }

        _scoreText.AddScore(score);
    }

    /// <summary>
    /// X座標
    /// </summary>
    /// <value>The x.</value>
    public float X
    {
        set
        {
            Vector3 pos = transform.position;
            pos.x = value;
            transform.position = pos;
        }
        get { return transform.position.x; }
    }

    /// <summary>
    /// Y座標
    /// </summary>
    /// <value>The y.</value>
    public float Y
    {
        set
        {
            Vector3 pos = transform.position;
            pos.y = value;
            transform.position = pos;
        }
        get { return transform.position.y; }
    }

    /// <summary>
    /// サイズの初期設定
    /// </summary>
    public void InitSize()
    {
        var w = SpriteWidth / 2;
        var h = SpriteHeight / 2;
        SetSize(w, h);
    }

    /// <summary>
    /// 矩形コリジョンの幅.
    /// </summary>
    /// <value>The width of the box collider.</value>
    public float BoxColliderWidth
    {
        get { return BoxCollider.size.x; }
        set
        {
            var size = BoxCollider.size;
            size.x = value;
            BoxCollider.size = size;
        }
    }

    /// <summary>
    /// 矩形コリジョンの高さ.s
    /// </summary>
    /// <value>The height of the box collider.</value>
    public float BoxColliderHeight
    {
        get { return BoxCollider.size.y; }
        set
        {
            var size = BoxCollider.size;
            size.y = value;
            BoxCollider.size = size;
        }
    }

    /// <summary>
    /// Gets or sets the collision radius.
    /// </summary>
    /// <value>The collision radius.</value>
    public float CollisionRadius
    {
        get { return CircleCollider.radius; }
        set { CircleCollider.radius = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:TokenProperty"/> circle collider enabled.
    /// </summary>
    /// <value><c>true</c> if circle collider enabled; otherwise, <c>false</c>.</value>
    public bool CircleColliderEnabled
    {
        get { return CircleCollider.enabled; }
        set { CircleCollider.enabled = value; }
    }

    /// <summary>
    /// Sets the size of the box collider.
    /// </summary>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    public void SetBoxColliderSize(float w, float h)
    {
        BoxCollider.size.Set(w, h);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:TokenController"/> box collider enabled.
    /// </summary>
    /// <value><c>true</c> if box collider enabled; otherwise, <c>false</c>.</value>
    public bool BoxColliderEnabled
    {
        get { return BoxCollider.enabled; }
        set { BoxCollider.enabled = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:TokenProperty"/> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public bool Visible
    {
        get { return Renderer.enabled; }
        set { Renderer.enabled = value; }
    }

    /// <summary>
    /// Gets the width of the sprite.
    /// </summary>
    /// <value>The width of the sprite.</value>
    public float SpriteWidth
    {
        get { return Renderer.bounds.size.x; }
    }

    public float SpriteHeight
    {
        get { return Renderer.bounds.size.y; }
    }

    /// <summary>
    /// Pixcelses the per unit.
    /// </summary>
    /// <returns>The per unit.</returns>
    public float PixcelsPerUnit
    {
        get { return Renderer.sprite.pixelsPerUnit; }
    }

    /// <summary>
    /// XFs the ot pixels per unit.
    /// </summary>
    /// <returns>The ot pixels per unit.</returns>
    public float XForPixelsPerUnit
    {
        get { return X * PixcelsPerUnit; }
    }

    /// <summary>
    /// XFs the ot pixels per unit.
    /// </summary>
    /// <returns>The ot pixels per unit.</returns>
    public float YForPixelsPerUnit
    {
        get { return Y * PixcelsPerUnit; }
    }

    /// <summary>
    /// サイズを指定
    /// </summary>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public void SetSize(float width, float height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// スケール値を設定.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public void SetScale (float x, float y)
    {
      Vector3 scale = transform.localScale;
      scale.Set (x, y, (x + y) / 2);
      transform.localScale = scale;
    }

    /// <summary>
    /// 画面外か判定
    /// </summary>
    /// <returns><c>true</c>, if outside was ised, <c>false</c> otherwise.</returns>
    public bool IsOutside()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        if (pos.x < min.x || pos.y < min.y)
        {
            return true;
        }
        if (pos.x > max.x || pos.y > max.y)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 座標を足し込む.
    /// </summary>
    /// <param name="dx">Dx.</param>
    /// <param name="dy">Dy.</param>
    public void AddPosition(float dx, float dy)
    {
        X += dx;
        Y += dy;
    }

    /// <summary>
    /// 座標を設定する.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public void SetPosition(float x, float y)
    {
        Vector3 pos = transform.position;
        pos.Set(x, y, 0);
        transform.position = pos;
    }

    /// <summary>
    /// スケール値を足し込む.
    /// </summary>
    /// <param name="d">D.</param>
    public void AddScale(float d)
    {
        Vector3 scale = transform.localScale;
        scale.x += d;
        scale.y += d;
        transform.localScale = scale;
    }

    /// <summary>
    /// スケール値をかける.
    /// </summary>
    /// <param name="d">D.</param>
    public void MulScale(float d)
    {
        transform.localScale *= d;
    }

    /// <summary>
    /// アルファ値をかける
    /// </summary>
    /// <param name="d">D.</param>
    public void MulAlpha(float d)
    {
        Alpha *= d;
    }

    /// <summary>
    /// 移動量を設定.
    /// </summary>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public void SetVelocity(float direction, float speed)
    {
        Vector2 v;
        v.x = AppUtil.CosEx(direction) * speed;
        v.y = AppUtil.SinEx(direction) * speed;
        RigidBody.velocity = v;
    }

    /// <summary>
    /// 移動量を設定(X/Y).s
    /// </summary>
    /// <param name="vx">Vx.</param>
    /// <param name="vy">Vy.</param>
    public void SetVelocityXY(float vx, float vy)
    {
        Vector2 v;
        v.x = vx;
        v.y = vy;
        RigidBody.velocity = v;
    }

    /// <summary>
    /// 移動量をかける.
    /// </summary>
    /// <param name="d">D.</param>
    public void MulVelocity(float d)
    {
        RigidBody.velocity *= d;
    }

    /// <summary>
    /// 色設定.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    public void SetColor(float r, float g, float b)
    {
        var c = Renderer.color;
        c.r = r;
        c.g = g;
        c.b = b;
        Renderer.color = c;
    }

    /// <summary>
    /// 移動して画面内に収めるようにする.
    /// </summary>
    /// <param name="v">V.</param>
    public void ClampScreenAndMove(Vector2 v)
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        pos += v;

        // 画面内に収まるように制限をかける.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // プレイヤーの座標を反映.
        transform.position = pos;
    }

    /// <summary>
    /// 画面内に収めるようにする.
    /// </summary>
    public void ClampScreen()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        // 画面内に収まるように制限をかける.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // プレイヤーの座標を反映.
        transform.position = pos;
    }

    /// <summary>
    /// スケール値(X)
    /// </summary>
    /// <value>The scale x.</value>
    public float ScaleX
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.x; }
    }

    /// <summary>
    /// スケール値(Y)
    /// </summary>
    /// <value>The scale y.</value>
    public float ScaleY
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.y = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.y; }
    }

    /// <summary>
    /// スケール値(X/Y)
    /// </summary>
    /// <value>The scale.</value>
    public float Scale
    {
        get
        {
            Vector3 scale = transform.localScale;
            return (scale.x + scale.y) / 2.0f;
        }
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            scale.y = value;
            transform.localScale = scale;
        }
    }

    /// <summary>
    /// 移動量(X).
    /// </summary>
    /// <value>The vx.</value>
    public float VX
    {
        get { return RigidBody.velocity.x; }
        set
        {
            Vector2 v = RigidBody.velocity;
            v.x = value;
            RigidBody.velocity = v;
        }
    }

    /// <summary>
    /// 移動量(Y).
    /// </summary>
    /// <value>The vy.</value>
    public float VY
    {
        get { return RigidBody.velocity.y; }
        set
        {
            Vector2 v = RigidBody.velocity;
            v.y = value;
            RigidBody.velocity = v;
        }
    }

    /// <summary>
    /// 方向.
    /// </summary>
    /// <value>The direction.</value>
    public float Direction
    {
        get
        {
            Vector2 v = RigidBody.velocity;
            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }
    }

    /// <summary>
    /// 速度.
    /// </summary>
    /// <value>The speed.</value>
    public float Speed
    {
        get
        {
            Vector2 v = RigidBody.velocity;
            return Mathf.Sqrt(v.x * v.x + v.y * v.y);
        }
    }

    /// <summary>
    /// 重力.
    /// </summary>
    /// <value>The gravity scale.</value>
    public float GravityScale
    {
        get { return RigidBody.gravityScale; }
        set { RigidBody.gravityScale = value; }
    }

    /// <summary>
    /// 回転角度.
    /// </summary>
    /// <value>The angle.</value>
    public float Angle
    {
        set { transform.eulerAngles = new Vector3(0, 0, value); }
        get { return transform.eulerAngles.z; }
    }

    /// <summary>
    /// アルファ値
    /// </summary>
    /// <value>The alpha.</value>
    public float Alpha
    {
        set
        {
            var c = Renderer.color;
            c.a = value;
            Renderer.color = c;
        }
        get
        {
            var c = Renderer.color;
            return c.a;
        }
    }

    /// <summary>
    /// 画面の左下のワールド座標を取得する.
    /// </summary>
    /// <returns>The world minimum.</returns>
    /// <param name="noMergin">If set to <c>true</c> no mergin.</param>
    public Vector2 GetWorldMin(bool noMergin = false)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        if (noMergin)
        {
            // そのまま返す.
            return min;
        }

        // 自身のサイズを考慮する.
        min.x += Width;
        min.y += Height;
        return min;
    }

    /// <summary>
    /// 画面右上のワールド座標を取得する.s
    /// </summary>
    /// <returns>The world max.</returns>
    /// <param name="noMergin">If set to <c>true</c> no mergin.</param>
    public Vector2 GetWorldMax(bool noMergin = false)
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        if (noMergin)
        {
            // そのまま返す.
            return max;
        }

        // 自身のサイズを考慮する.
        max.x -= Width;
        max.y -= Height;
        return max;
    }

    /// <summary>
    /// ソーティングレイヤーを取得する
    /// </summary>
    /// <value>The sorting layer.</value>
    public string SortingLayer
    {
        get { return Renderer.sortingLayerName; }
        set { Renderer.sortingLayerName = value; }
    }

    /// <summary>
    /// ソーティングオーダを取得する
    /// </summary>
    /// <value>The sorting order.</value>
    public int SortingOrder
    {
        get { return Renderer.sortingOrder; }
        set { Renderer.sortingOrder = value; }
    }

    /// <summary>
    /// 消滅（メモリから削除）
    /// </summary>
    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// アクティブにする
    /// </summary>
    public virtual void Revive()
    {
        gameObject.SetActive(true);
        Exists = true;
        Visible = true;
    }

    /// <summary>
    /// 消滅する（オーバーライド可能）
    /// ただし base.Vanish()を呼ばないと消滅しなくなることに注意
    /// </summary>
    public virtual void Vanish()
    {
        VanishCannotOverride();
    }

    /// <summary>
    /// 消滅する（オーバーライド禁止）
    /// </summary>
    public void VanishCannotOverride()
    {
        gameObject.SetActive(false);
        Exists = false;
    }

    /// <summary>
    /// HPバー
    /// </summary>
    protected HPBarMax _hpBarMax = null;
    protected HPBarMax HPBarMax
    {
        get
        {
            if (_hpBarMax == null)
            {
                GameObject prefub = Resources.Load("Prefabs/" + "HPBarMax") as GameObject;
                GameObject g = Object.Instantiate(prefub, new Vector3(X, Y + 0.1f + SpriteHeight / 2, 0), Quaternion.identity);
                g.transform.parent = this.transform;
                _hpBarMax = g.GetComponent<HPBarMax>();
            }

            return _hpBarMax;
        }
    }
}
