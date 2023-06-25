﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace AmazingAssets.CurvedWorld.Example
{
    [ExecuteAlways]
    public class TransformDynamicPosition : MonoBehaviour
    {
        public CurvedWorld.CurvedWorldController curvedWorldController;

        public Transform parent;

        public Vector3 offset;
        public bool recalculateRotation;

        public void Init(CurvedWorldController curvedWorldController)
        {
            this.curvedWorldController = curvedWorldController;
        }

        void Start()
        {
            if (parent == null)
                parent = transform.parent;
        }

        // void Update()
        // {
        //     if (parent == null || curvedWorldController == null)
        //     {
        //         //Do nothing
        //     }
        //     else
        //     {
        //         //Transform position
        //         transform.position = curvedWorldController.TransformPosition(parent.position + offset);
        //     
        //     
        //         //Transform normal (calcualte rotation)
        //         if (recalculateRotation)
        //             transform.rotation =
        //                 curvedWorldController.TransformRotation(parent.position + offset, parent.forward, parent.right);
        //     }
        // }

        private void LateUpdate()
        {
            if (parent == null || curvedWorldController == null)
            {
                if (Application.isPlaying && curvedWorldController == null)
                    curvedWorldController = FindObjectOfType<CurvedWorldController>();
            }
            else
            {
                transform.position = curvedWorldController.TransformPosition(parent.position + offset);
                if (recalculateRotation)
                    transform.rotation =
                        curvedWorldController.TransformRotation(parent.position + offset, parent.forward, parent.right);
            }
        }
        // private void FixedUpdate()
        // {
        //     if (parent == null || curvedWorldController == null)
        //     {
        //         //Do nothing
        //     }
        //     else
        //     {
        //         //Transform position
        //         transform.position = curvedWorldController.TransformPosition(parent.position + offset);
        //
        //
        //         //Transform normal (calcualte rotation)
        //         if (recalculateRotation)
        //             transform.rotation =
        //                 curvedWorldController.TransformRotation(parent.position + offset, parent.forward, parent.right);
        //     }
        // }
    }
}