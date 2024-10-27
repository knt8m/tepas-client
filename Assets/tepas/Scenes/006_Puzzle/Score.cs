using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

/// <summary>
/// ゲーム内のスコア管理
/// </summary>
public class Score : MonoBehaviour
{
    [BoxGroup("スコア"), LabelText("残数"), Required]
    public TextMeshProUGUI remainText; // 残数 (正解すべき数)

    [BoxGroup("スコア"), LabelText("ミス数"), Required]
    public TextMeshProUGUI missText; // ミス数

    /// <summary>
    /// 残数とミス数をデフォルトの値に設定
    /// </summary>
    /// <param name="remain">初期設定の残数</param>
    /// <param name="miss">初期設定のミス数</param>
    public void Default(int remain = 0, int miss = 0)
    {
        remainText.text = Convert.ToString(remain);
        missText.text = Convert.ToString(miss);
    }

    /// <summary>
    /// ミスした
    /// </summary>
    public void Miss()
    {
        int nowMiss = Convert.ToInt32(missText.text);
        missText.text = Convert.ToString(++nowMiss);

    }

    /// <summary>
    /// 正解した
    /// </summary>
    public void Good()
    {
        int nowRemain = Convert.ToInt32(remainText.text);
        remainText.text = Convert.ToString(--nowRemain);
    }
}
