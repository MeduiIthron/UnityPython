using UnityEngine;
using UnityPython;

public class PythonHelloWorld : MonoBehaviour
{
    private void Start()
    {
        var engine = PythonEnvironment.CreateEngine();
        var scope = engine.CreateScope();
        var source = "print('Hello World')";
        engine.Execute(source, scope);
    }
}
