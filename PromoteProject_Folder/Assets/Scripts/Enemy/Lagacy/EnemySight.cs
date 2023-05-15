using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]         // ����
    [Range(0f, 360f)]               // Range ��Ʈ����Ʈ�� ����
    [SerializeField] private float m_horizontalViewAngle = 55f;       // �þ߰�
    [SerializeField] private float m_viewRadius = 3f;       // �þ��� ũ��
    [Range(-100f, 100f)]
    [SerializeField] protected float m_viewRotateZ = 0f;       // �þ߰��� ȸ����Ű�µ� �̿� : ĳ������ ����� �þ��� ������ �޶���� ��� ���

    // �� �� �ִ� Ÿ�ٰ� �þ߸� ���θ��� ������Ʈ�� ����
    [SerializeField] private LayerMask m_viewTargetMask;
    [SerializeField] private LayerMask m_viewObstacleMask;

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float m_horizontalViewHalfAngle = 0f;                      // �þ߰��� ��

    void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
    }
    protected void setViewRotateZ(float f)
    {
        m_viewRotateZ = f;
    }

    protected void reverseViewRotateZ()
    {
        m_viewRotateZ *= -1;
    }

    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;       // �Է��� Angle�� Local Direction���� ��ȯ ��Ű�� ���� ���
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }

    private void OnDrawGizmos()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

        Vector3 originPos = transform.position;

        Gizmos.DrawWireSphere(originPos, m_viewRadius);         // �ν��� �� �ִ� ���� �׸���

        Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);       // �þ߰��� ���Ⱚ���� ��ȯ
        Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);         // m_viewRotateZ�� ���� �þ߰��� �� ȸ����ų �� �ִ�.
        Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

        Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.red);
        // Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);
        Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.red);
    }

    public Collider2D[] FindViewTargets()
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask);

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= m_horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask); // m_viewObstacleMask
                if (rayHitedTarget)
                {
                    Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);
                    Debug.DrawLine(originPos, targetPos, Color.red);
                }
            }
        }

        if (hitedTargetContainer.Count > 0)
        {
            Debug.Log(hitedTargetContainer.ToArray());
            return hitedTargetContainer.ToArray();
        }
        else
            return null;
    }
}
