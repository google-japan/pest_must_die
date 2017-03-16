using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hachi2 : MonoBehaviour {
    // ヒットポイント
    public int hp = 1;

    // スコアのポイント
    public int point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;

    // 増殖済み
    static bool zoushokuZumi = false;

    IEnumerator Start()
    {

        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        // ローカル座標のY軸のマイナス方向に移動する
        Move(transform.up * -1);

        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            if (!zoushokuZumi && spaceship.transform.position.x < 2)
            {
                zoushokuZumi = true;
                cloneHachi(0, 50);
                cloneHachi(0, -50);
                cloneHachi(0, 25);
                cloneHachi(0, -25);

            }

            // shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    private void cloneHachi(int x, int y)
    {
        GameObject clone = Instantiate(gameObject) as GameObject;
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
    }

    // 機体の移動
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed * 4;
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
        hp = hp - bullet.power;

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
