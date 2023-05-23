using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    [Header("체력바 관련")]
    public GameObject prfHpBar;
    public GameObject canvas;
    public float height = 1.7f;

    [Header("적 스탯 관련")]
    public int enemyID;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;
    public int force;       // 밀려나는 정도
    RectTransform hpBar;
    Image nowHpbar;
    EnemyMove enemyMove;
    Rigidbody2D rigid;

    Animator anim;

    private void SetEnemyStatus(int _enemyID, int _maxHp, int _atkDmg, int _atkSpeed, int _force)
    {
        enemyID = _enemyID;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        force = _force;
    }
    
    private void Awake()
    {
        anim = GetComponent<Animator>();

        // 적 체력바 생성
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        if (enemyID == 1)
        {
            SetEnemyStatus(1, 100, 20, 1, 100000);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
        enemyMove = gameObject.GetComponent<EnemyMove>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 체력바 이동
        Vector3 _hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        
        die();
    }

    public void attacked(int damage)
    {
        nowHp -= damage;
    }

    void die()
    {
        if (nowHp <= 0) {
            enemyMove.CancelInvoke();
            enemyMove.nextMove = 0;
            anim.SetBool("isDead", true);
            StartCoroutine(deadDelay());
        }
    }

    IEnumerator deadDelay()
    {
        yield return new WaitForSeconds(1.0f);
        PlayerStat.instance.exp += 10;
        Destroy(hpBar.gameObject);
        Destroy(gameObject);
    }
}
