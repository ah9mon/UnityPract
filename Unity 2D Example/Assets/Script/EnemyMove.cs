using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    public int nextMove;


    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
        spriteRenderer = GetComponent<SpriteRenderer>();    
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Platform check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Vector2 vec = new Vector2(rigid.position.x + rigid.velocity.normalized.x / 2, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(new Vector2(rigid.position.x, rigid.position.y), Vector3.left, new Color(1, 1, 1));
        Debug.DrawRay(new Vector2(rigid.position.x, rigid.position.y), Vector3.right, new Color(1, 1, 1));
        RaycastHit2D rayhitDown = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("PlatForm"));
        RaycastHit2D rayhitForward = Physics2D.Raycast(vec, Vector3.forward, 1, LayerMask.GetMask("PlatForm"));

        if (rayhitDown.collider == null)
        {
            Turn();
        } else if (rayhitForward.collider != null)
        {
            Turn();
            Debug.Log("벽입니다");
        }



    }

    // 재귀
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        // sorite animation
        animator.SetInteger("WalkSpeed", nextMove);

        // 뒤집기
        if (nextMove !=  0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // 재귀
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Sprite Flip Y
        spriteRenderer.flipY = true;
        // Collider Disable
        capsuleCollider.enabled = false;
        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        // Destory
        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
