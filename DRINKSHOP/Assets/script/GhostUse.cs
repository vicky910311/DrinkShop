using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostUse : MonoBehaviour
{
    float now, move;
    float MinX = -1.5f, MinY = -1.4f, MaxX = 1.5f, MaxY = 1.4f;
    // Start is called before the first frame update
    void Start()
    {
        now = Time.time;
        move = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > now + move)
        {
            float X = Random.Range(0.5f, 1f);
            float Y = Random.Range(0.5f, 1f);
            int Dx = Random.Range(0, 2) == 1 ? -1 : 1;
            int Dy = Random.Range(0, 2) == 1 ? -1 : 1;

            float GoX = transform.position.x + X * Dx;
            GoX = Mathf.Clamp(GoX, MinX, MaxX);
            float GoY = transform.position.y + Y * Dy;
            GoY = Mathf.Clamp(GoY, MinY, MaxY);

            Vector3 Go = new Vector3(GoX, GoY, 0);
            float Dis = Vector3.Distance(Go, transform.position);
            if (Dis > 1f)
            {
                this.GetComponent<SpriteRenderer>().flipX = Go.x - transform.position.x > 0 ? true : false;
                transform.DOMove(Go, 2f).SetEase(Ease.Linear);
                now = Time.time;
            }
        }
    }
    void OnMouseDown()
    {
        PlayerDataManager.self.Player.CatchGhost++;
        Destroy(gameObject);
        Debug.Log(name.ToString() + "被點了一下");
    }
}
