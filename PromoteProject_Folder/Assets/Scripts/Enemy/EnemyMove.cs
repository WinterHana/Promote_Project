using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // �ൿ��ǥ�� ������ ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        Invoke("Think", 5);     // 5�ʸ��� Think �Լ� ����
    }
    void Start()
    {
            
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);         // -1�̸� ����, 0�̸� ���߱�, 1�̸� ���������� �̵�

        Invoke("Think", 5);                     // 5�ʸ��� Think �Լ� ���� by ����Լ�
    }
    void move()
    {
        // �� �������� ������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // �÷���(����) Ȯ��
        // Vector2 frontVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawLine(rigid.position, Vector2.down, new Color(1, 0, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("tiles"));

        if (rayHit.collider == null)
        {
            Debug.Log("�Ѿ�����!");
            nextMove = nextMove * (-1);
            CancelInvoke();
            Invoke("Think", 5);
        }
    }
}
