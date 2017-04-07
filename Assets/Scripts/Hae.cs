using UnityEngine;
using System.Collections;

public class Hae : MonoBehaviour {
    // ヒットポイント
    public int hp = 1;

    // スコアのポイント
    public int point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;

    IEnumerator Start()
    {

        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            Move(transform.up * 1);
            // shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    public float a = 0.0f;
    public float b = 0.0f;
    // 機体の移動
    public void Move(Vector2 d)
    {
        a = a + 60.0f;
        b = b + 60.0f;

        float xKakudo = a;
        float yKakudo = b;
        float speed = spaceship.speed * 3;
        float x = Mathf.Cos(Mathf.Deg2Rad * xKakudo) * speed;
        float y = Mathf.Sin(Mathf.Deg2Rad * yKakudo) * speed;
        if(x < 0)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * xKakudo) * spaceship.speed * 5;
        }
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
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
           // spaceship.GetAnimator().SetTrigger("Damage");

        }
    }
}
