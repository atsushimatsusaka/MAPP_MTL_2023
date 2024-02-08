using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMgr : MonoBehaviour
{
    public List<Floting> floating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WholeKillTrigger()
    {
        for (int i = 0; i < floating.Count; i++)
        {
            floating[i].KillTrigger();
        }
    }

    public void WholeSetTrigger()
    {
        for (int i = 0; i < floating.Count; i++)
        {
            floating[i].SetTrigger();
        }
    }
}
