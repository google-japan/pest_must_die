using UnityEngine;
using System.Collections;

public class Hae1 : MonoBehaviour
{
    // ヒットポイント
    public int hp = 2;

    // スコアのポイント
    public int point = 200;

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

    public float xKakudo = 100.0f;
    public float yKakudo = 30.0f;
    // 機体の移動
    public void Move(Vector2 d)
    {

        if (transform.position.y > -1 && transform.position.y < 1)
        {
        }
        else if (transform.position.y >= 1)
        {
            xKakudo = -100.0f;
            yKakudo = -30.0f;
        }else if (transform.position.y <= -1)
        {
            xKakudo = 100.0f;
            yKakudo = 30.0f;
        }

        float speed = spaceship.speed;
        float x = Mathf.Cos(Mathf.Deg2Rad * xKakudo) * speed*12;
        float y = Mathf.Sin(Mathf.Deg2Rad * yKakudo) * speed*4;

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
            spaceship.GetAnimator().SetTrigger("Damage");

        }
    }
}
