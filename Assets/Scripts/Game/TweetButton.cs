using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class TweetButton : MonoBehaviour
{
    public void OnClick()
    {
        Application.OpenURL(CreateTweetURL());
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
