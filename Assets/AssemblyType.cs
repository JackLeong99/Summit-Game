using System.Reflection;

public static class AssemblyTypes
{
    public static System.Type[] ReturnTypes()
    {
        return Assembly.GetExecutingAssembly().GetTypes();
    }
}