using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class RoslynCompilerCode : MonoBehaviour
{
    [SerializeField]
    private string[] namespaces;

    [SerializeField]
    [TextArea(5, 12)]
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
        //Debug.Log("1");
        try
        {
            sourceCode = $"{updatedCode ?? sourceCode} {additionalCode} "; //"}"+
            ScriptState<object> result = CSharpScript.RunAsync(sourceCode, SetDefaultImports()).Result;   //Allows to complie and run at runtime
            //Debug.Log("3");
            foreach (string var in resultVars)
            {
                resultInfo += $"{result.GetVariable(var).Name}: {result.GetVariable(var).Value}\n";

            }
            //Debug.Log("4");
            OnRunCodeCompleted?.Invoke();
            //Debug.Log("5");
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }
    
    private ScriptOptions SetDefaultImports()
    {
        //Debug.Log("2");
        return ScriptOptions.Default
            .WithImports(namespaces.Select(n => n.Replace("using", string.Empty).Trim()))
            .AddReferences(
                typeof(UnityEngine.Physics).Assembly,
                typeof(MonoBehaviour).Assembly,
                typeof(Debug).Assembly
            );
    }

}
