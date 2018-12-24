using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private long _frameCount = 0;

    private StageDataReader _stageDataR = new StageDataReader();
    private StageDefineReader _stageDefineR = new StageDefineReader();

    /// <summary>
    /// プレハブリスト
    /// </summary>
    private Dictionary<string, GameObject> _prefabDictionary = new Dictionary<string, GameObject>();

    /// <summary>
    /// ポーズ中フラグ
    /// </summary>
    private static bool _pause = false;
    public static bool IsPause
    {
        get { return _pause; }
        set { _pause = value; }
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        // ステージデータ読み込み
        _stageDataR.Load("Stage1.txt");
        _stageDefineR.Load("Define.txt");

        // 各インスタンス生成
        PlayerShot.InitTokenManager(32);
        EnemyShot1.InitTokenManager(256);
        EnemyShot2.InitTokenManager(256);
        Particle.InitTokenManager(512);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // スタートキー押下
        if (Input.GetKeyUp(KeyCode.JoystickButton8))
        {
            SwitchPouse();
        }

        if (IsPause)
        {
            return;
        }

        if (_frameCount >= 999999999999)
        {
            return;
        }

        // 現フレームに対応するゲーム情報を取得して反映する
        AddFrameData(_frameCount);

        _frameCount++;
    }

    /// <summary>
    /// 指定フレームのゲーム情報を反映
    /// </summary>
    /// <param name="frame">Frame.</param>
    private void AddFrameData(float frame)
    {
        List<string> stageDataList = _stageDataR.GetStageDataListByFrame(frame);
        if (stageDataList == null)
        {
            return;
        }

        foreach (string id in stageDataList)
        {
            StageDefine stageDefine = _stageDefineR.GetStageDataByID(id);
            if (stageDefine == null)
            {
                continue;
            }

            GameObject prefub = GetPrefubByName(stageDefine.PrefubName);
            if (prefub == null)
            {
                continue;
            }

            float x = stageDefine.PointX;
            float y = stageDefine.PointY;
            GameObject g = Object.Instantiate(prefub, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// 指定したプレハブ名のプレハブを取得
    /// </summary>
    /// <returns>The prefub by name.</returns>
    /// <param name="prefubName">Prefub name.</param>
    private GameObject GetPrefubByName(string prefubName)
    {
        // キャッシュから取得
        GameObject result = null;
        _prefabDictionary.TryGetValue(prefubName, out result);
        if (result != null)
        {
            return result;
        }

        // 読み込む
        GameObject g = Resources.Load("Prefabs/" + prefubName) as GameObject;
        if (g != null)
        {
            _prefabDictionary.Add(prefubName, g);
            return g;
        }

        Debug.Log("[" + prefubName + "] " + "が存在しない");
        return null;
    }

    /// <summary>
    /// ポーズ状態を切り替える
    /// </summary>
    private void SwitchPouse()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            _pause = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            _pause = false;
        }
    }
}
