using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class AssemblyTypes
{
    public static System.Type[] ReturnTypes()
    {
        List<System.Type> types = new List<System.Type>();
        var assemblies = System.AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic);
        foreach (var item in assemblies)
        {
            switch (true)
            {
                case bool x when item.CodeBase.Contains("Assembly-CSharp.dll"):
                    types = types.Concat(item.GetTypes()).ToList();
                    break;
            }
        }


        return types.ToArray();
        //Debug.Log(Assembly.GetExecutingAssembly().CodeBase);
        //return Assembly.GetExecutingAssembly().GetTypes();
    }

    public static System.Type[] GetAllTypes()
    {
        System.Type[] types = ReturnTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(ScriptableObject)) select type).ToArray();

        return possible;
    }
}