using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class TitleEnemy : Enemy
{
    public new void FixedUpdate()
    {
        HitAnimation();
        Eyes();
        Mouth();
        Move();
    }

    //“®‚«
    protected new void Move()
    {
        Debug.Log("a");
        float speed = 3.0f;
        float range = 3.0f;
        Vector3 position = this.transform.position;
        position.y = _standardPosition.position.y + Mathf.Sin(Time.time* speed) * range;
        this.transform.position = position;

        range = 10.0f;
        Vector3 angle = this.transform.eulerAngles;
        //angle.x = Mathf.Sin(Time.time * speed)*range*0.5f;
        angle.y = Mathf.Sin(Time.time * speed*0.4f)*range;
        angle.z = Mathf.Cos(Time.time * speed)*range;
       // angle = angle.normalized;
       // angle *= range;
        this.transform.eulerAngles = angle;
    }
}
