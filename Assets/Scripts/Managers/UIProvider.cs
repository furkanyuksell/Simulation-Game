using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIProvider
{
    private static Dictionary<Type, IProvidable> ProvidableServices = new Dictionary<Type, IProvidable>();
    public static T GetService<T>() where T : class, IProvidable
    {
        if (ProvidableServices.ContainsKey(typeof(T)))
        {
            return ProvidableServices[typeof(T)] as T;
        }
        else
        {
            Debug.LogError("Service " + typeof(T) + " not found");
            return null;
        }
    }

    public static T Register<T>(T service) where T : class, IProvidable
    {
        ProvidableServices.Add(typeof(T), service);
        return service;
    }

    public static UIManager GetUIManager
    {
        get{ return GetService<UIManager>();}
    }

    public static UITextbox GetUITextbox
    {
        get{ return GetService<UITextbox>();}
    }
}
