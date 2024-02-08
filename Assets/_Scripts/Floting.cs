using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Floting : MonoBehaviour
{
    [SerializeField] private bool autoPlay = false;
    [SerializeField] private bool upwardAtFirst = true;
    [SerializeField] private float movingRange = 0.1f;
    [SerializeField] private float defaDuration = 4f;
    [SerializeField] private Vector2 durationRandom = new Vector2(-0.1f, 0.1f);
    [SerializeField] private Vector2 waitingTimeRandom = new Vector2(0f, 1f);
    private Vector3 defaPos;
    private float defaPosY;
    public bool kill = false;

    // Start is called before the first frame update
    void Start()
    {
        defaPos = gameObject.transform.position;

        defaPosY = defaPos.y;
        if (upwardAtFirst) defaPosY = defaPosY - movingRange / 2;
        else defaPosY = defaPosY + movingRange / 2;

        if (autoPlay) SetTrigger();
        //transform.position = new Vector3(defaPos.x, defaPosY, defaPos.z);
        //StartCoroutine(Floating());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    kill = true;
        //}

        if (kill)
        {
            gameObject.transform.DOKill();
            kill = false;
        }
    }

    IEnumerator Floating()
    {
        var waitVal = Random.Range(waitingTimeRandom.x, waitingTimeRandom.y);

        yield return new WaitForSeconds(waitVal);

        var randomVal = Random.Range(durationRandom.x, durationRandom.y);

        var movingVector = 1;
        if (upwardAtFirst) movingVector = 1;
        else movingVector = -1;

        gameObject.transform.DOMoveY(defaPosY + movingRange * movingVector, defaDuration + randomVal).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        //Debug.Log(gameObject.name + " : " + defaDuration + randomVal);
    }

    public void KillTrigger()
    {
        kill = true;
    }

    public void SetTrigger()
    {
        transform.position = new Vector3(defaPos.x, defaPosY, defaPos.z);
        StartCoroutine(Floating());
    }
}
