using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uribo : MonoBehaviour
{
    //RigidBody2Dのコンポーネントを入れる変数を宣言する
    Rigidbody2D rb2D;

    //BoxCollider2Dのコンポーネントを入れる変数を宣言する
    BoxCollider2D bc2D;

    //横に進むスピード
    public float speed = 10.0f;

    //ジャンプのスピード
    public float jumpSpeed = 5.0f;

    //地面のレイヤー
    public LayerMask groundLayer;

    void Start()
    {
        //Rigidbody2Dのコンポーネントを取得する
        rb2D = GetComponent<Rigidbody2D>();

        //BoxCollider2Dのコンポーネントを取得する
        bc2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //ボタンの操作を検知したら
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //右向きにする
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //左向きにする
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void FixedUpdate()
    {
        //横方向の入力値を取得する
        float hori = Input.GetAxis("Horizontal");

        //Rigidbody2Dに力を加える
        rb2D.velocity = new Vector2(hori * speed, rb2D.velocity.y);

        //上ボタンが押された時、地面に接地していたらジャンプする
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
        {
            //Rigidbody2Dに力を加える
            rb2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    //地面についているかどうか
    bool isGrounded()
    {
        //Layを飛ばす長さ
        float layLength = (bc2D.size.y / 2) + 0.1f;

        //Raycastを飛ばして地面を調べる
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, layLength, groundLayer);

        //Rayを可視化
        //Debug.DrawRay(transform.position, new Vector3(0, -layLength, 0), Color.blue, 1);

        //接地しているかどうかを返す
        if (hit)
        {
            return hit;
        }
        else
        {
            return false;
        }
    }
}