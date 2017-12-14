using System;
using System.Collections.Generic;
using System.Text;

public class Component
{

}

public class Behaviour : Component
{

}

public class MonoBehaviour : Behaviour
{

}


class GameObject
{
    List<Component> m_componentList = new List<Component>();

    public Transform transform
    {
        get 
        { 
            return GetComponent<Transform>(); 
        }
    }

    public T AddComponent<T>() where T : Component
    {
        T ret = default(T);
        m_componentList.Add(ret);
        return ret;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach(var iter in m_componentList)
        {
            if(iter is T)
            {
                return iter as T;
            }
        }

        return null;
    }
}

