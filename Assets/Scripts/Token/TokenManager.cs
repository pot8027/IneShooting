using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager<T> where T : TokenController
{
    /// <summary>
    /// インスタンスの数
    /// </summary>
    private int _size = 0;

    /// <summary>
    /// プレハブ
    /// </summary>
    private GameObject _prefab = null;

    /// <summary>
    /// インスタンスリスト
    /// </summary>
    private List<T> _pool = null;

    public delegate void FuncT(T t);

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="prefabName">Prefab name.</param>
    /// <param name="size">Size.</param>
    public TokenManager(string prefabName, int size)
    {
        // プレハブ読み込み
        if (!LoadAndSetPrefub(prefabName, size))
        {
            return;
        }

        _pool = new List<T>();

        // サイズ指定があれば指定分のトークンを作成する
        if (size > 0)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject g = Object.Instantiate(_prefab, new Vector3(), Quaternion.identity) as GameObject;
                T obj = g.GetComponent<T>();

                // この段階ではプールしておくだけ
                obj.VanishCannotOverride();
                _pool.Add(obj);
            }
        }
    }

    /// <summary>
    /// インスタンスを取得する
    /// </summary>
    /// <returns>The add.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public T Add(float x, float y, float direction = 0.0f, float speed = 0.0f)
    {
        foreach (T obj in _pool)
        {
            // 未使用のオブジェクトがあればそれを取得する
            if (obj.Exists == false)
            {

                return _Recycle(obj, x, y, direction, speed);
            }
        }

        // サイズ指定がない場合は自動で拡張
        if (_size == 0)
        {

            GameObject g = Object.Instantiate(_prefab, new Vector3(), Quaternion.identity) as GameObject;
            T obj = g.GetComponent<T>();
            _pool.Add(obj);
            return _Recycle(obj, x, y, direction, speed);
        }
        return null;
    }

    /// <summary>
    /// 存在するトークンに指定した処理を行う
    /// </summary>
    /// <param name="func">Func.</param>
    public void FuncForEachExist(FuncT func)
    {
        foreach (var obj in _pool)
        {
            if (obj.Exists)
            {
                func(obj);
            }
        }
    }

    /// <summary>
    /// オブジェクトを再利用する
    /// </summary>
    /// <returns>The recycle.</returns>
    /// <param name="obj">Object.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    private T _Recycle(T obj, float x, float y, float direction, float speed)
    {
        // 復活
        obj.Revive();
        obj.SetPosition(x, y);

        // Rigidbody2Dがあるときのみ速度を設定する
        if (obj.RigidBody != null)
        {
            obj.SetVelocity(direction, speed);
        }

        obj.Angle = 0;

        // Order in Layerをインクリメントして設定する
        //obj.SortingOrder = _order;
        //_order++;
        return obj;
    }

    /// <summary>
    /// プレハブを読み込み、セットします
    /// </summary>
    /// <returns>true=読み込み成功, false=読み込み失敗</returns>
    /// <param name="prefabName">Prefab name.</param>
    /// <param name="size">Size.</param>
    private bool LoadAndSetPrefub(string prefabName, int size)
    {
        _size = size;
        _prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;

        if (_prefab == null)
        {
            Debug.LogError("Not found prefab. name=" + prefabName);
            return false;
        }

        return true;
    }
}
