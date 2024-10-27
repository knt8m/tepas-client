using UnityEngine;

public class ScoreData : MonoBehaviour
{
    public int remain;
    public int miss;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetRemain()
    {
        return remain;
    }

    public int GetMiss()
    {
        return remain;
    }


    /// <summary>
    /// 残数とミス数をデフォルトの値に設定
    /// </summary>
    /// <param name="remain">初期設定の残数</param>
    /// <param name="miss">初期設定のミス数</param>
    public void Default(int remain = 0, int miss = 0)
    {
        this.remain = remain;
        this.miss = miss;
    }

    /// <summary>
    /// ミス数を1追加する
    /// </summary>
    public void Miss()
    {
        miss = miss++;
    }

    /// <summary>
    /// 残数を1つ減らす。
    /// </summary>
    public void Correct()
    {
        remain = remain--;
    }



}

