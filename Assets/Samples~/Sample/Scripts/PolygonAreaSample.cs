/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PolygonAreaSample.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  09/02/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Mathematics.Sample
{
    [RequireComponent(typeof(LineRenderer))]
    public class PolygonAreaSample : MonoBehaviour
    {
        public MeshRenderer tangMonk;
        public Material greenMat;
        public Material redMat;
        public float speed = 1.25f;
        bool isInside = false;
        List<Vector2> ps;

        [ContextMenu("Build Area")]
        void Start()
        {
            var line = GetComponent<LineRenderer>();
            line.useWorldSpace = false;

            SetVertexCount(line, transform.childCount + 1);
            line.SetPosition(transform.childCount, transform.GetChild(0).localPosition);

            var index = 0;
            ps = new List<Vector2>();
            foreach (Transform child in transform)
            {
                line.SetPosition(index, child.localPosition);
                ps.Add(new Vector2(child.position.x, child.position.z));
                index++;
            }
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                tangMonk.transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                tangMonk.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                tangMonk.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                tangMonk.transform.Translate(Vector3.back * speed * Time.deltaTime);
            }

            var p = new Vector2(tangMonk.transform.position.x, tangMonk.transform.position.z);
            var isIn = PolygonUtility.Contains(ps, p);
            if (isIn != isInside)
            {
                isInside = isIn;
                tangMonk.material = isInside ? greenMat : redMat;
            }
        }

        void SetVertexCount(LineRenderer lineRenderer, int count)
        {
#if UNITY_5_6_OR_NEWER
            lineRenderer.positionCount = count;
#else
            lineRenderer.SetVertexCount(count);
#endif
        }
    }
}