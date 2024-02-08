using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetMTMgr : MonoBehaviour
{
    [SerializeField] private bool maskMode = false;

    // 親オブジェクト
    private Transform _parent;

    // 親含めて子オブジェクトを再帰的に取得するかどうか
    [SerializeField] private bool _includeParent = true;


    [SerializeField] Material maskMat;
    int num_obj = 0;
    int num_mr = 0;

    private void Start()
    {
        if (!maskMode) return;

        _parent = gameObject.transform;

        // 子オブジェクトを取得する
        var children = GetChildrenRecursive(_parent, _includeParent);

        // 取得した子オブジェクト名をログ出力
        for (var i = 0; i < children.Length; i++)
        {
            //num_obj += 1;
            //Debug.Log(children[i].name);
            //Debug.Log("the number of All objects is " + num_obj);

            var mr = children[i].gameObject.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                //num_mr += 1;
                //Debug.Log("the number of MeshRenderer is " + num_mr);

                mr.material = maskMat;
            }
            else
            {
                var smr = children[i].gameObject.GetComponent<SkinnedMeshRenderer>();
                if (smr != null)
                {
                    smr.material = maskMat;
                }
            }     
        }
    }

    // parent直下の子オブジェクトを再帰的に取得する
    private static Transform[] GetChildrenRecursive(Transform parent, bool includeParent = true)
    {
        // 親を含む子オブジェクトを再帰的に取得
        // trueを指定しないと非アクティブなオブジェクトを取得できないことに注意
        var parentAndChildren = parent.GetComponentsInChildren<Transform>(true);

        if (includeParent)
        {
            // 親を含む場合はそのまま返す
            return parentAndChildren;
        }

        // 子オブジェクトの格納用配列作成
        var children = new Transform[parentAndChildren.Length - 1];

        // 親を除く子オブジェクトを結果にコピー
        Array.Copy(parentAndChildren, 1, children, 0, children.Length);

        // 子オブジェクトが再帰的に格納された配列
        return children;
    }
}
