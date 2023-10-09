using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public virtual void Initialized()
    {
        
    }

    public virtual void Updated()
    {
   
    }

    public void SetThisObjectToBaseManager()
    {
        GameObject baseManagerObject = GameObject.Find("BaseManager");
        BaseManager baseManagerCS = baseManagerObject.GetComponent<BaseManager>();
        baseManagerCS.SetObject(this.gameObject);
    }
}
