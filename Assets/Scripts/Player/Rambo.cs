﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rambo : Player
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private Animator effect;
    [SerializeField]
    private GameObject bullet;
    public override void SetUp()
    {
        gun.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder + 1;
        effect.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }
    public override void Die()
    {
        base.Die();
    }
    private void Update()
    {
        if (circleRange.CheckRange())
        {
            base.Action();
            Action();
        }
        else
        {
            acting = false;
            Moverment();
        }
        time += Time.deltaTime;
    }
    public override void Action()
    {
        if (time >= waiting - atkspeed)
        {
            time = 0;
            ActionCoroutine();
        }
    }
    void ActionCoroutine()
    {
        if (circleRange.target[0] != null)
        {
            Vector2 dir = circleRange.target[0].transform.position - gun.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(0, 0, angle);
            effect.SetTrigger("Action");
            GameObject newBullet = Instantiate(bullet, effect.transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().SetUp(10f, atk, angle, gun.GetComponent<SpriteRenderer>().sortingOrder - 1, circleRange.target[0]);
        }
        source.Play();
    }
}
