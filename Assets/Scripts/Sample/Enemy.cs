//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : Token {

    //public Sprite Spr0;
    //public Sprite Spr1;
    //public Sprite Spr2;
    //public Sprite Spr3;
    //public Sprite Spr4;
    //public Sprite Spr5;

    //public static TokenMgr<Enemy> parent = null;

    //// ステータス
    //int _hp = 0;
    //public int HP
    //{
    //    get { return _hp; }
    //}

    //int _id = 0;

    //// プレイヤー
    //public static Player target = null;


    //private void Update()
    //{
    //    if (_id == 4)
    //    {
    //        Vector2 min = GetWorldMin();
    //        Vector2 max = GetWorldMax();

    //        // 上下ではみ出したら跳ね返る
    //        if (Y < min.y || max.y < Y)
    //        {
    //            ClampScreen();
    //            VY *= -1;
    //        }
    //        // 左右ではみ出したら消滅する
    //        if (X < min.x || max.x < X)
    //        {
    //            Vanish();
    //        }

    //        Angle = Direction;
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (_id <= 3)
    //    {
    //        MulVelocity(0.93f);
    //    }

    //}

    //public void SetParam(int id)
    //{
    //    // 前回のコルーチン終了
    //    if (_id != 0)
    //    {
    //        StopCoroutine("_Uppdate" + _id);
    //    }

    //    if (id != 0)
    //    {
    //        StartCoroutine("_Update" + id);
    //    }

    //    _id = id;

    //    int[] hps = { 50, 30, 30, 30, 30, 30 };
    //    Sprite[] sprs = { Spr0, Spr1, Spr2, Spr3, Spr4, Spr5 };

    //    _hp = hps[id];
    //    SetSprite(sprs[id]);
    //}

    //public static Enemy Add(int id, float x, float y, float direction, float speed)
    //{
    //    Enemy e = parent.Add(x, y, direction, speed); ;
    //    if (e == null)
    //    {
    //        return null;
    //    }

    //    e.SetParam(id);

    //    return e;
    //}

    //IEnumerator _Update0()
    //{
    //    Debug.Log("_Update0が呼ばれてる");
    //    yield return new WaitForSeconds(2.0f);
    //}

    //IEnumerator _Update1()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(2.0f);

    //        float dir = GetAim();
    //        DoBullet(dir, 2);
    //    }
    //}

    //IEnumerator _Update2()
    //{
    //    // Bulletのparentがnullの場合があるので、少し待つ
    //    yield return new WaitForSeconds(2.0f);

    //    float dir = 0;
    //    while (true)
    //    {
    //        DoBullet(dir, 2);
    //        dir += 16;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    //IEnumerator _Update3()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(2.0f);
    //        DoBullet(180 - 2, 2);
    //        DoBullet(180, 2);
    //        DoBullet(180 + 2, 2);
    //    }
    //}

    //IEnumerator _Update4()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //}

    //IEnumerator _Update5()
    //{
    //    const float ROT = 5.0f;
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.02f);

    //        float dir = Direction;
    //        float aim = GetAim();
    //        float delta = Mathf.DeltaAngle(dir, aim);

    //        if (Mathf.Abs(delta) < ROT)
    //        {

    //        }
    //        else if (delta > 0)
    //        {
    //            dir += ROT;
    //        }
    //        else
    //        {
    //            dir -= ROT;
    //        }

    //        SetVelocity(dir, Speed);
    //        Angle = dir;

    //        if (IsOutside())
    //        {
    //            Vanish();
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // Layer名取得
    //    string name = LayerMask.LayerToName(collision.gameObject.layer);
    //    if (name.Equals("Shot"))
    //    {
    //        Shot s = collision.GetComponent<Shot>();
    //        s.Vanish();

    //        Damage(1);
    //    }
    //}

    //public float GetAim()
    //{
    //    float dx = target.X - X;
    //    float dy = target.Y - Y;
    //    return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    //}

    //bool Damage(int v)
    //{
    //    _hp -= v;
    //    if (_hp <= 0)
    //    {
    //        Vanish();

    //        for (int i = 0; i < 4; i++)
    //        {
    //            Particle.Add(X, Y);
    //        }

    //        Sound.PlaySe("destroy");

    //        if (_id == 0)
    //        {
    //            // ボスを倒したらザコ敵と弾を消す
    //            Enemy.parent.ForEachExist(e => e.Damage(9999));

    //            if (Bullet.parent != null)
    //            {
    //                Bullet.parent.Vanish();
    //            }
    //        }


    //        return true;
    //    }
    //    return false;
    //}

    //void DoBullet(float direction, float speed)
    //{
    //    Bullet.Add(X, Y, direction, speed);
    //}
//}
