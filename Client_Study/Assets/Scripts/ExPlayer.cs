using UnityEngine;

public class ExPlayer : MonoBehaviour
{
    private int health = 100; // 플레이어 체력

    // 플레이어가 피해를 받을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        // 플레이어 체력 감소
        health -= damage;

        print("현재 플레이어 체력 : " + health);
        // 플레이어 체력이 0이하로 떨어졌을 때 플레이어 사망 처리
        if (health<=0)
        {
            Die();
        }
    }


    private void Die()
    {
        print("병원에서 리스폰하기");
        // 사망 처리 로직 추가

    }
}
