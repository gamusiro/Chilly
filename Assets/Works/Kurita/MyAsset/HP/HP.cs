using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : BaseObject
{
    private int _hp;//hp�c��
    [SerializeField] private Animator _animatior;//�A�j���[�V��������

    public override void Initialized()
    {
        _hp = 8;
    }

    public override void Updated()
    {
        
    }

    public void Hit()
    {
        _hp--;
    }

    private void HPAnimation()
    {
        _animatior.SetBool("nextAnimation", true);
    }
}
