using UnityEngine;
using System.Collections;

public class KinchoruManager : MonoBehaviour {
    private Spaceship spaceship;
    public readonly int DefaultPower = 1; 
    public readonly int PowerUpPoint = 1; /* 1アイテム取得したときのパワーアップポイント */
    public readonly int MaxPower = 3;

    public void setSpaceShip(Spaceship s)
    {
        spaceship = s;
    }

    public void SetDefaultPower()
    {
        GameObject g = spaceship.bullet; // ゲームオブジェクトを取得
        Bullet b = g.GetComponent<Bullet>(); // コンポーネントを取得（これでスクリプトが取得出来るっぽい）
        b.power = DefaultPower;
        b.powerchou = DefaultPower;
        b.powerhachi = DefaultPower;
    }

    public void powerUp(string layerName)
    {
        GameObject g = spaceship.bullet; // ゲームオブジェクトを取得
        Bullet b = g.GetComponent<Bullet>(); // コンポーネントを取得（これでスクリプトが取得出来るっぽい）
        
        if (layerName.Contains("Hae"))
        {
            if (b.power < MaxPower)
            {
                b.power += PowerUpPoint;
            } 
        }else if(layerName.Contains("Hachi")){
            if (b.powerhachi < MaxPower)
            {
                b.powerhachi += PowerUpPoint;
            }
        }
        else{
            if (b.powerchou < MaxPower)
            {
                b.powerchou += PowerUpPoint;
            }
        }
    }
    
}
