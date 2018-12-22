using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 各インスタンス生成
        //Player.InitTokenManager(1);
        PlayerShot.InitTokenManager(16);
        EnemyShot1.InitTokenManager(64);
        Particle.InitTokenManager(512);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
