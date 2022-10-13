using System.Linq;
using System.Reflection;
using UnityEngine;

public static class AssemblyTypes
{
    public static System.Type[] ReturnTypes()
    {
        return Assembly.GetExecutingAssembly().GetTypes();
    }

    public static System.Type[] GetAllTypes()
    {
        System.Type[] types = ReturnTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(ScriptableObject)) select type).ToArray();

        return possible;
    }
}