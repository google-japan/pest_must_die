using UnityEngine;

public class Manager : MonoBehaviour
{
	// Playerプレハブ
	public GameObject player;
	
	// タイトル
	private GameObject title;

    // ランキング
    public GameObject ranking;

    void Start ()
	{
		// Titleゲームオブジェクトを検索し取得する
		title = GameObject.Find ("Title");
        // ランキングを取得し非表示とする
        ranking = GameObject.Find("Ranking");
        ranking.SetActive(false);
    }
	
	void Update ()
	{
        // タイトル画面で、Xキーが押されたらゲームスタート。
        if (IsTitle() == true && Input.GetKeyDown(KeyCode.X))
        {
            GameStart();
        }

        // ランキング画面で、Tキーが押されたらタイトルへ
        if (IsRanking() == true && Input.GetKeyDown(KeyCode.T))
        {
            GameTitle();
        }
    }

    void GameTitle()
    {
        title.SetActive(true);
        ranking.SetActive(false);
    }

    void GameStart ()
	{
        // ゲームスタート時に、タイトル・ランキングを非表示にしてプレイヤーを作成する
        title.SetActive(false);
        ranking.SetActive(false);
        GameObject obj = Instantiate (player, player.transform.position, player.transform.rotation);
        obj.name = player.name;
	}
	
	public void GameOver ()
	{
        // ゲームオーバー時に、ランキングを表示する
        ranking.SetActive (true);
        // スコア登録
        FindObjectOfType<Score>().Save();
    }

    public bool IsPlaying()
    {
        // ゲーム中かどうかはタイトル/ランキングの表示/非表示で判断する
        return (title.activeSelf == false && ranking.activeSelf == false);
    }

    public bool IsRanking()
    {
        // ランキング画面かどうか判定
        return ranking.activeSelf == true;
    }

    public bool IsTitle()
    {
        // タイトル画面かどうか判定
        return title.activeSelf == true;
    }
}