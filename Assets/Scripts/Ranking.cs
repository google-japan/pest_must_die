using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour
{
    // GUIText
    public GUIText ranking1GUIText;
    public GUIText ranking2GUIText;
    public GUIText ranking3GUIText;
    public GUIText ranking4GUIText;
    public GUIText ranking5GUIText;

    // ランキングスコア
    private int ranking1;
    private int ranking2;
    private int ranking3;
    private int ranking4;
    private int ranking5;

    // PlayerPrefsで保存するためのキー
    public string ranking1Key = "ranking1";
    public string ranking2Key = "ranking2";
    public string ranking3Key = "ranking3";
    public string ranking4Key = "ranking4";
    public string ranking5Key = "ranking5";

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        ranking1GUIText.text = "1位 : " + ranking1.ToString();
        ranking2GUIText.text = "2位 : " + ranking2.ToString();
        ranking3GUIText.text = "3位 : " + ranking3.ToString();
        ranking4GUIText.text = "4位 : " + ranking4.ToString();
        ranking5GUIText.text = "5位 : " + ranking5.ToString();
    }

    private void Initialize()
    {
        // ランキングスコアを取得
        ranking1 = PlayerPrefs.GetInt(ranking1Key, 0);
        ranking2 = PlayerPrefs.GetInt(ranking2Key, 0);
        ranking3 = PlayerPrefs.GetInt(ranking3Key, 0);
        ranking4 = PlayerPrefs.GetInt(ranking4Key, 0);
        ranking5 = PlayerPrefs.GetInt(ranking5Key, 0);
    }

    // スコアを受け取ってランキング更新
    public void UpdateRanking(int score)
    {
        // 現在のランキング・スコアを降順に並べて更新
        ArrayList rankingList = new ArrayList();

        rankingList.Add(ranking1);
        rankingList.Add(ranking2);
        rankingList.Add(ranking3);
        rankingList.Add(ranking4);
        rankingList.Add(ranking5);
        rankingList.Add(score);

        rankingList.Sort();
        rankingList.Reverse();

        PlayerPrefs.SetInt(ranking1Key, (int)rankingList[0]);
        PlayerPrefs.SetInt(ranking2Key, (int)rankingList[1]);
        PlayerPrefs.SetInt(ranking3Key, (int)rankingList[2]);
        PlayerPrefs.SetInt(ranking4Key, (int)rankingList[3]);
        PlayerPrefs.SetInt(ranking5Key, (int)rankingList[4]);
        PlayerPrefs.Save();

        Initialize();
    }
}
