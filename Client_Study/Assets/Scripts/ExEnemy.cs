using UnityEngine;

public class ExEnemy : MonoBehaviour
{
    public ExPlayer targetPlayer;

    public int damage = 20;

    public void AttackPlayer(ExPlayer player)
    {
        player.TakeDamage(damage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("�÷��̾� ����");
            if (targetPlayer != null)
            {
                AttackPlayer(targetPlayer);
            }
        }
    }
}
