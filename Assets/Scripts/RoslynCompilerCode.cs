using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Callables;

public class RoslynCompilerCode : MonoBehaviour
{
    [SerializeField]
    private string[] namespaces;

    [SerializeField]
    [TextArea(20, 12)]
    private string sourceCode;

    [SerializeField]
    [TextArea(5, 12)]
    private string additionalCode;

    [SerializeField]
    private UnityEvent OnRunCodeCompleted;

    [SerializeField]
    private string[] resultVars;

    [SerializeField]
    [TextArea(5, 20)]
    private string resultInfo;


    public void RunCode(string updatedCode = null)
    {
        Debug.Log("Executing RunCode...");

        updatedCode = string.IsNullOrEmpty(updatedCode)? null : updatedCode;
        
        try
        {
            sourceCode = $"{updatedCode ?? sourceCode} {additionalCode} "; //"}"+
            ScriptState<object> result = CSharpScript.RunAsync(sourceCode, SetDefaultImports()).Result;   //Allows to complie and run at runtime
            
            foreach (string var in resultVars)
            {
                resultInfo += $"{result.GetVariable(var).Name}: {result.GetVariable(var).Value}\n";
            
            }
            
            OnRunCodeCompleted?.Invoke();
            
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
            
        }
    }
    
    private ScriptOptions SetDefaultImports()
    {
       
        return ScriptOptions.Default
            .WithImports(namespaces.Select(n => n.Replace("using", string.Empty).Trim()))
            .AddReferences(
                typeof(UnityEngine.Physics).Assembly,
                typeof(MonoBehaviour).Assembly,
                typeof(Debug).Assembly,
                typeof(CallableMethods).Assembly
            );
    }
}
