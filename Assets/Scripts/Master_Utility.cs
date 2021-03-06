﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Utility : MonoBehaviour {

    public static Master_Utility Instance;
    public GameObject m_MyCanvas;
    public List<GameObject> m_Pools;

    private Vector3 m_Diff;
    private Vector3 t_Vector3;
    private Vector2 t_Vector2;
    private float m_RotZ;
    private Transform t_Transform;
    private int t_I, t_Idx;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }else Destroy(gameObject);
    }


    //Usable functions in public

    public Transform f_LookAt2D(Transform p_YourTransform) {
        t_Transform = p_YourTransform;
        m_Diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - t_Transform.position;
        if (Mathf.Abs(m_Diff.x) > 0.5f && Mathf.Abs(m_Diff.y) > 0.5f) {
            m_Diff.Normalize();
            m_RotZ = Mathf.Atan2(m_Diff.y, m_Diff.x) * Mathf.Rad2Deg;
            t_Transform.rotation = Quaternion.Euler(0f, 0f, m_RotZ - 90);
        }
        return t_Transform;
    }

    public GameObject f_ObjPool(GameObject p_Object, Vector3 p_Position, Quaternion p_Rotation, bool p_RequireCanvas) {
        t_Idx = f_GetPoolIdx(p_Object);

        if (t_Idx < 0) {
            m_Pools.Add(Instantiate(p_Object, p_Position, p_Rotation, p_RequireCanvas? m_MyCanvas.transform : transform));
            return m_Pools[m_Pools.Count - 1];
        }

        m_Pools[t_Idx].transform.position = p_Position;
        m_Pools[t_Idx].transform.rotation = p_Rotation;
        m_Pools[t_Idx].SetActive(true);

        return m_Pools[t_Idx];
    }

    public Vector3 f_SetVector3(float p_X, float p_Y, float p_Z) {
        t_Vector3.x = p_X;
        t_Vector3.y = p_Y;
        t_Vector3.z = p_Z;

        return t_Vector3;
    }

    public Vector2 f_SetVector2(float p_X, float p_Y) {
        t_Vector2.x = p_X;
        t_Vector2.y = p_Y;

        return t_Vector2;
    }


    //Supporting functions in private
    private int f_GetPoolIdx(GameObject p_Object) {
        if (m_Pools.Count == 0) return -1;
        for (t_I = 0; t_I < m_Pools.Count; t_I++) if (!m_Pools[t_I].activeSelf && m_Pools[t_I].tag == p_Object.tag) return t_I;
        return -1;
    }
   
}
