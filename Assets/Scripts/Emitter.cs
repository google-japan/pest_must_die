using UnityEngine;
using System.Collections;
using System;

public class Emitter : MonoBehaviour
{
	// Waveプレハブを格納する
	public GameObject[] waves;
	
	// 現在のWave
	private int currentWave;
	
	// Managerコンポーネント
	private Manager manager;
    private System.Random rnd;

    IEnumerator Start ()
	{
		
		// Waveが存在しなければコルーチンを終了する
		if (waves.Length == 0) {
			yield break;
		}
		
		// Managerコンポーネントをシーン内から探して取得する
		manager = FindObjectOfType<Manager>();
        rnd = new System.Random();

		while (true) {
			
			// タイトル表示中は待機
			while(manager.IsPlaying() == false) {
				yield return new WaitForEndOfFrame ();
			}
			
			// Waveを作成する
			GameObject g = (GameObject)Instantiate (waves [rnd.Next(waves.Length)], transform.position, Quaternion.identity);
			
			// WaveをEmitterの子要素にする
			g.transform.parent = transform;
			
			// Waveの子要素のEnemyが全て削除されるまで待機する
			while (g.transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}
			
			// Waveの削除
			Destroy (g);			
		}
	}
}