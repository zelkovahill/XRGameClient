using UnityEngine;

public class ExPlayer : MonoBehaviour
{
    private int health = 100; // �÷��̾� ü��

    // �÷��̾ ���ظ� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        // �÷��̾� ü�� ����
        health -= damage;

        print("���� �÷��̾� ü�� : " + health);
        // �÷��̾� ü���� 0���Ϸ� �������� �� �÷��̾� ��� ó��
        if (health<=0)
        {
            Die();
        }
    }


    private void Die()
    {
        print("�������� �������ϱ�");
        // ��� ó�� ���� �߰�

    }
}
