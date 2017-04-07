using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hachi : MonoBehaviour {

    // ヒットポイント
    public int hp = 1;

    // スコアのポイント
    public int point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;

    public GameObject targetObject = null;

    IEnumerator Start()
    {
        targetObject = GameObject.Find("Player");
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            Move(transform.position);
            // shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    // 機体の移動
    public void Move(Vector2 d)
    {
        if (targetObject == null)
        {
            return;
        }

        float speed = spaceship.speed;
        GetComponent<Rigidbody2D>().velocity = targetObject.transform.position * speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // レイヤー名がBullet (Player)以外の時は何も行わない
        if (layerName != "Bullet (Player)") return;

        // PlayerBulletのTransformを取得
        Transform playerBulletTransform = c.transform.parent;

        // Bulletコンポーネントを取得
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

        // ヒットポイントを減らす
        hp = hp - bullet.powerhachi;

        // 弾の削除
        Destroy(c.gameObject);

        // ヒットポイントが0以下であれば
        if (hp <= 0)
        {
            // スコアコンポーネントを取得してポイントを追加
            FindObjectOfType<Score>().AddPoint(point);

            // 爆発
            spaceship.Explosion();

            // エネミーの削除
            Destroy(gameObject);

        }
        else
        {
            // Damageトリガーをセット
            spaceship.GetAnimator().SetTrigger("Damage");

        }
    }
}
