using UnityEngine;

public class Helper {

    public static Transform FindTransform(Transform parent, string childName)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (child.name == childName)
            {
                return child;
            }
            if (child.childCount > 0)
            {
                var obj =  FindTransform(child, childName);
                if (obj != null)
                    return obj;
            }
        }
        return null;
    }
}
