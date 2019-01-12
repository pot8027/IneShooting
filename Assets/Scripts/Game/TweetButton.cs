using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.Networking;


public class TweetButton : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void OpenNewWindow(string url);

    public void OnPointerDown()
    {
#if UNITY_EDITOR
        Application.OpenURL(CreateTweetURL());
#else
        OpenNewWindow(CreateTweetURL());
#endif
    }

    private string CreateTweetURL()
    {
        StringBuilder sb = new StringBuilder();

        // TwitterURL
        sb.Append(@"https://twitter.com/intent/tweet");
        sb.Append("?");

        // シェアURL
        sb.Append("url=" + CreateShareURL());
        sb.Append("&");

        // ツイート
        sb.Append("text=" + CreateTweet());

        return sb.ToString();
    }

    private string CreateShareURL()
    {
        return UnityWebRequest.EscapeURL(@"https://pot8027.github.io/IneShoot/");
    }

    private string CreateTweet()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("稲ジューティングクリア！！");

        // レベル
        int level = GameManager.StageNo;
        sb.Append(" [LEVEL]" + level);

        // スコア
        float score = GameManager.Score;
        sb.Append("  [スコア]" + score);

        // ハッシュタグ
        sb.Append(" #稲シューティング");

        return UnityWebRequest.EscapeURL(sb.ToString());
    }
}
