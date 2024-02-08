using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetMTMgr : MonoBehaviour
{
    [SerializeField] private bool maskMode = false;

    // �e�I�u�W�F�N�g
    private Transform _parent;

    // �e�܂߂Ďq�I�u�W�F�N�g���ċA�I�Ɏ擾���邩�ǂ���
    [SerializeField] private bool _includeParent = true;


    [SerializeField] Material maskMat;
    int num_obj = 0;
    int num_mr = 0;

    private void Start()
    {
        if (!maskMode) return;

        _parent = gameObject.transform;

        // �q�I�u�W�F�N�g���擾����
        var children = GetChildrenRecursive(_parent, _includeParent);

        // �擾�����q�I�u�W�F�N�g�������O�o��
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

    // parent�����̎q�I�u�W�F�N�g���ċA�I�Ɏ擾����
    private static Transform[] GetChildrenRecursive(Transform parent, bool includeParent = true)
    {
        // �e���܂ގq�I�u�W�F�N�g���ċA�I�Ɏ擾
        // true���w�肵�Ȃ��Ɣ�A�N�e�B�u�ȃI�u�W�F�N�g���擾�ł��Ȃ����Ƃɒ���
        var parentAndChildren = parent.GetComponentsInChildren<Transform>(true);

        if (includeParent)
        {
            // �e���܂ޏꍇ�͂��̂܂ܕԂ�
            return parentAndChildren;
        }

        // �q�I�u�W�F�N�g�̊i�[�p�z��쐬
        var children = new Transform[parentAndChildren.Length - 1];

        // �e�������q�I�u�W�F�N�g�����ʂɃR�s�[
        Array.Copy(parentAndChildren, 1, children, 0, children.Length);

        // �q�I�u�W�F�N�g���ċA�I�Ɋi�[���ꂽ�z��
        return children;
    }
}
