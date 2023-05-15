using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]         // 제목
    [Range(0f, 360f)]               // Range 어트리뷰트로 제한
    [SerializeField] private float m_horizontalViewAngle = 55f;       // 시야각
    [SerializeField] private float m_viewRadius = 3f;       // 시야의 크기
    [Range(-100f, 100f)]
    [SerializeField] protected float m_viewRotateZ = 0f;       // 시야각을 회전시키는데 이용 : 캐릭터의 방향과 시야의 방향이 달라야할 경우 사용

    // 볼 수 있는 타겟과 시야를 가로막는 오브젝트를 지정
    [SerializeField] private LayerMask m_viewTargetMask;
    [SerializeField] private LayerMask m_viewObstacleMask;

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float m_horizontalViewHalfAngle = 0f;                      // 시야각의 반

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
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;       // 입력한 Angle을 Local Direction으로 변환 시키기 위한 계산
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }

    private void OnDrawGizmos()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

        Vector3 originPos = transform.position;

        Gizmos.DrawWireSphere(originPos, m_viewRadius);         // 인식할 수 있는 범위 그리기

        Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);       // 시야값을 방향값으로 변환
        Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);         // m_viewRotateZ에 따라 시야각을 더 회전시킬 수 있다.
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
