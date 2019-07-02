# UnityPython

UnityPython is a plugin for Unity3D that provides support for running Python code in Unity Projects
Special thanks to the developers of IronPython who developed the open-source integration of Python and .NET, which this plugin uses

## Setup
Install .unitypackage in your project
Then, go to Edit > Project Settings > Player > Other Settings > Configuration and change Scripting Runtime Version to "Experimental (.NET 4.6 Equivalent)"

##Usage##
An example is provided below. More examples can be found in the Example/ folder

```
using UnityEngine;
using UnityPython;

public class TestPythonClass : MonoBehaviour
{
    private void Start()
    {
        var engine = PythonEnvironment.CreateEngine();
        var scope = engine.CreateScope();
        var source = "print('Hello World')";
        engine.Execute(source, scope);
    }
}
```

##Information##
Current Python version :: IronPython 2.7.9