using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQ.Helpers.Heirarchy
{
    public class Heirarchy
    {
        public static void PassToHeirarchyEnd(GameObject gameObject, Action<GameObject> action)
        {
            action(gameObject);
            foreach (Transform transform in gameObject.transform)
            {
                PassToHeirarchyEnd(transform.gameObject, action);
            }
        }
    }
}
