using System;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.CSharp;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This code example creates a graph using a CodeCompileUnit and  
/// generates source code for the graph using the CSharpCodeProvider.
/// </summary>
public class CodeCreator : MonoBehaviour {
        
    /// <summary>
    /// Define the compile unit to use for code generation. 
    /// </summary>
    CodeCompileUnit targetUnit;

    /// <summary>
    /// The only class in the compile unit. This class contains 2 fields,
    /// 3 properties, a constructor, an entry point, and 1 simple method. 
    /// </summary>
    CodeTypeDeclaration targetClass;

    /// <summary>
    /// The name of the file to contain the source code.
    /// </summary>
    private const string outputFileName = "Assets/Resources/TestClass.cs";
    private const string assembliesRepository = "C:/Program Files/Unity/Hub/Editor/2019.1.12f1/Editor/Data/Managed/UnityEngine/";

    /// <summary>
    /// Define the class.
    /// </summary>
    public CodeCreator() {
        targetUnit = new CodeCompileUnit();
        CodeNamespace samples = new CodeNamespace();
        samples.Imports.Add(new CodeNamespaceImport("System"));
        samples.Imports.Add(new CodeNamespaceImport("UnityEngine"));
        targetClass = new CodeTypeDeclaration("TestClass");
        targetClass.BaseTypes.Add("MonoBehaviour");
        targetClass.IsClass = true;
        targetClass.TypeAttributes = TypeAttributes.Public;
        samples.Types.Add(targetClass);
        targetUnit.Namespaces.Add(samples);
    }

    ///// <summary>
    ///// Adds two fields to the class.
    ///// </summary>
    //public void AddFields() {
    //    // Declare the widthValue field.
    //    CodeMemberField widthValueField = new CodeMemberField();
    //    widthValueField.Attributes = MemberAttributes.Private;
    //    widthValueField.Name = "widthValue";
    //    widthValueField.Type = new CodeTypeReference(typeof(System.Double));
    //    widthValueField.Comments.Add(new CodeCommentStatement(
    //        "The width of the object."));
    //    targetClass.Members.Add(widthValueField);

    //    // Declare the heightValue field
    //    CodeMemberField heightValueField = new CodeMemberField();
    //    heightValueField.Attributes = MemberAttributes.Private;
    //    heightValueField.Name = "heightValue";
    //    heightValueField.Type =
    //        new CodeTypeReference(typeof(System.Double));
    //    heightValueField.Comments.Add(new CodeCommentStatement(
    //        "The height of the object."));
    //    targetClass.Members.Add(heightValueField);
    //}
    ///// <summary>
    ///// Add three properties to the class.
    ///// </summary>
    //public void AddProperties() {
    //    // Declare the read-only Width property.
    //    CodeMemberProperty widthProperty = new CodeMemberProperty();
    //    widthProperty.Attributes =
    //        MemberAttributes.Public | MemberAttributes.Final;
    //    widthProperty.Name = "Width";
    //    widthProperty.HasGet = true;
    //    widthProperty.Type = new CodeTypeReference(typeof(System.Double));
    //    widthProperty.Comments.Add(new CodeCommentStatement(
    //        "The Width property for the object."));
    //    widthProperty.GetStatements.Add(new CodeMethodReturnStatement(
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "widthValue")));
    //    targetClass.Members.Add(widthProperty);

    //    // Declare the read-only Height property.
    //    CodeMemberProperty heightProperty = new CodeMemberProperty();
    //    heightProperty.Attributes =
    //        MemberAttributes.Public | MemberAttributes.Final;
    //    heightProperty.Name = "Height";
    //    heightProperty.HasGet = true;
    //    heightProperty.Type = new CodeTypeReference(typeof(System.Double));
    //    heightProperty.Comments.Add(new CodeCommentStatement(
    //        "The Height property for the object."));
    //    heightProperty.GetStatements.Add(new CodeMethodReturnStatement(
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "heightValue")));
    //    targetClass.Members.Add(heightProperty);

    //    // Declare the read only Area property.
    //    CodeMemberProperty areaProperty = new CodeMemberProperty();
    //    areaProperty.Attributes =
    //        MemberAttributes.Public | MemberAttributes.Final;
    //    areaProperty.Name = "Area";
    //    areaProperty.HasGet = true;
    //    areaProperty.Type = new CodeTypeReference(typeof(System.Double));
    //    areaProperty.Comments.Add(new CodeCommentStatement(
    //        "The Area property for the object."));

    //    // Create an expression to calculate the area for the get accessor 
    //    // of the Area property.
    //    CodeBinaryOperatorExpression areaExpression =
    //        new CodeBinaryOperatorExpression(
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "widthValue"),
    //        CodeBinaryOperatorType.Multiply,
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "heightValue"));
    //    areaProperty.GetStatements.Add(
    //        new CodeMethodReturnStatement(areaExpression));
    //    targetClass.Members.Add(areaProperty);
    //}

    ///// <summary>
    ///// Adds a method to the class. This method multiplies values stored 
    ///// in both fields.
    ///// </summary>
    //public void AddMethod() {
    //    // Declaring a ToString method
    //    CodeMemberMethod toStringMethod = new CodeMemberMethod();
    //    toStringMethod.Attributes =
    //        MemberAttributes.Private;
    //    toStringMethod.Name = "Start";
    //    toStringMethod.ReturnType =
    //        new CodeTypeReference(typeof(void));

    //    CodeFieldReferenceExpression widthReference =
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "Width");
    //    CodeFieldReferenceExpression heightReference =
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "Height");
    //    CodeFieldReferenceExpression areaReference =
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "Area");

    //    // Declaring a return statement for method ToString.
    //    CodeExpressionStatement returnStatement =
    //        new CodeExpressionStatement();

    //    // This statement returns a string representation of the width,
    //    // height, and area.
    //    string formattedOutput = "It works !!";
    //    returnStatement.Expression =
    //        new CodeMethodInvokeExpression(
    //        new CodeTypeReferenceExpression("Debug"), "Log",
    //        new CodePrimitiveExpression(formattedOutput));
    //    toStringMethod.Statements.Add(returnStatement);
    //    targetClass.Members.Add(toStringMethod);
    //}
    /// <summary>
    /// Add a constructor to the class.
    /// </summary>
    //public void AddConstructor() {
    //    // Declare the constructor
    //    CodeConstructor constructor = new CodeConstructor();
    //    constructor.Attributes =
    //        MemberAttributes.Public | MemberAttributes.Final;

    //    // Add parameters.
    //    constructor.Parameters.Add(new CodeParameterDeclarationExpression(
    //        typeof(System.Double), "width"));
    //    constructor.Parameters.Add(new CodeParameterDeclarationExpression(
    //        typeof(System.Double), "height"));

    //    // Add field initialization logic
    //    CodeFieldReferenceExpression widthReference =
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "widthValue");
    //    constructor.Statements.Add(new CodeAssignStatement(widthReference,
    //        new CodeArgumentReferenceExpression("width")));
    //    CodeFieldReferenceExpression heightReference =
    //        new CodeFieldReferenceExpression(
    //        new CodeThisReferenceExpression(), "heightValue");
    //    constructor.Statements.Add(new CodeAssignStatement(heightReference,
    //        new CodeArgumentReferenceExpression("height")));
    //    targetClass.Members.Add(constructor);
    //}

    ///// <summary>
    ///// Add an entry point to the class.
    ///// </summary>
    //public void AddEntryPoint() {
    //    CodeEntryPointMethod start = new CodeEntryPointMethod();
    //    CodeObjectCreateExpression objectCreate =
    //        new CodeObjectCreateExpression(
    //        new CodeTypeReference("TestClass"),
    //        new CodePrimitiveExpression(5.3),
    //        new CodePrimitiveExpression(6.9));

    //    // Add the statement:
    //    // "TestClass testClass = 
    //    //     new TestClass(5.3, 6.9);"
    //    start.Statements.Add(new CodeVariableDeclarationStatement(
    //        new CodeTypeReference("TestClass"), "testClass",
    //        objectCreate));

    //    // Creat the expression:
    //    // "testClass.ToString()"
    //    CodeMethodInvokeExpression toStringInvoke =
    //        new CodeMethodInvokeExpression(
    //        new CodeVariableReferenceExpression("testClass"), "ToString");

    //    // Add a System.Console.WriteLine statement with the previous 
    //    // expression as a parameter.
    //    start.Statements.Add(new CodeMethodInvokeExpression(
    //        new CodeTypeReferenceExpression("System.Console"),
    //        "WriteLine", toStringInvoke));
    //    targetClass.Members.Add(start);
    //}

    void AddField(MemberAttributes visibility, string fieldName, Type fieldType) {
        CodeMemberField memberField = new CodeMemberField();
        memberField.Attributes = visibility;
        memberField.Name = fieldName;
        memberField.Type = new CodeTypeReference(fieldType);
        targetClass.Members.Add(memberField);
    }

    void AddMethod(MemberAttributes visibility, string methodName, Type returnType) {
        CodeMemberMethod memberMethod = new CodeMemberMethod();
        memberMethod.Attributes = visibility;
        memberMethod.Name = methodName;
        memberMethod.ReturnType = new CodeTypeReference(returnType);

        //TODO change following code which change the color of a gameObject, to a generic code
        if (methodName == "Start") {
            memberMethod.Statements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("initColor"),
               new CodeVariableReferenceExpression("Color.blue")));
            memberMethod.Statements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("otherColor"),
               new CodeVariableReferenceExpression("Color.red")));
            targetClass.Members.Add(memberMethod);
            return;
        }
        /*
        if (Input.GetKeyDown(KeyCode.C)) {
            Renderer rend = GetComponent<Renderer>();
            if (rend.material.color == initColor) {
                rend.material.color = otherColor;
            } else {
                rend.material.color = initColor;
            }
        }
        */
        CodeStatement[] trueStatements = new CodeStatement[2];
        // Invokes the TestMethod method of the current type object.
        CodeMethodReferenceExpression methodRef1 = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetComponent", new CodeTypeReference(@"Renderer"));
        CodeMethodInvokeExpression invoke1 = new CodeMethodInvokeExpression(methodRef1);
        trueStatements[0] = new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(Renderer)), "rend",  invoke1);
        trueStatements[1] = new CodeConditionStatement(
            new CodeBinaryOperatorExpression( 
                new CodeFieldReferenceExpression( 
                    new CodeVariableReferenceExpression("rend"), "material.color"),
                CodeBinaryOperatorType.ValueEquality, new CodeVariableReferenceExpression("initColor")
            ), new CodeStatement[] {
                new CodeAssignStatement(new CodeVariableReferenceExpression("rend.material.color"), new CodeVariableReferenceExpression("otherColor")) 
            }, new CodeStatement[] {
                new CodeAssignStatement(new CodeVariableReferenceExpression("rend.material.color"), new CodeVariableReferenceExpression("initColor"))
            }
            );
        CodeConditionStatement ifStatement = new CodeConditionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(Input)), "GetKeyDown", new CodeVariableReferenceExpression("KeyCode.C")),
            trueStatements);
        memberMethod.Statements.Add(ifStatement);
        if (returnType != null) {}
        targetClass.Members.Add(memberMethod);
    }

    /// <summary>
    /// Generate CSharp source code from the compile unit.
    /// </summary>
    /// <param name="filename">Output file name</param>
    public void GenerateCSharpCode(string fileName) {
        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        CodeGeneratorOptions options = new CodeGeneratorOptions();
        options.BracingStyle = "C";
        using (StreamWriter sourceWriter = new StreamWriter(fileName)) {
            provider.GenerateCodeFromCompileUnit(
                targetUnit, sourceWriter, options);
        }
    }

    void Start() {
        AddField(MemberAttributes.Private, "initColor", typeof(Color));
        AddField(MemberAttributes.Private, "otherColor", typeof(Color));
        //AddProperties();
        AddMethod(MemberAttributes.Private, "Start", typeof(void));
        AddMethod(MemberAttributes.Private, "Update", typeof(void));
        GenerateCSharpCode(outputFileName);
        //bool a = CompileCSharpCode(outputFileName, "Assets/Scripts/TestClass.exe");
        Assembly assembly = CompileCSharpCode(outputFileName, "TestChangeColor.dll");
        Assembly.Load(assembly.FullName);
        //CodeTypeReference ref1 = new CodeTypeReference(targetClass.Name);
        GameObject.Find("Cube").AddComponent(assembly.GetType("TestClass"));
    }

    void Update() { }
    public static Assembly CompileCSharpCode(string sourceFile, string dllFile) {
        CSharpCodeProvider provider = new CSharpCodeProvider();

        // Build the parameters for source compilation.
        CompilerParameters cp = new CompilerParameters();

        // Add an assembly reference.
        cp.ReferencedAssemblies.Add("System.dll");
        cp.ReferencedAssemblies.Add(assembliesRepository + "UnityEngine.CoreModule.dll");

        // Generate an executable instead of
        // a class library.
        cp.GenerateExecutable = false;

        // Set the assembly file name to generate.
        cp.OutputAssembly = dllFile;

        // Save the assembly as a physical file.
        cp.GenerateInMemory = true;

        // Invoke compilation.
        CompilerResults cr = provider.CompileAssemblyFromFile(cp, sourceFile);

        if (cr.Errors.Count > 0) {
            // Display compilation errors.
            Console.WriteLine("Errors building {0} into {1}",
                sourceFile, cr.PathToAssembly);
            foreach (CompilerError ce in cr.Errors) {
                Debug.LogError(ce.ToString());
                Console.WriteLine();
            }
        } else {
            Console.WriteLine("Source {0} built into {1} successfully.",
                sourceFile, cr.PathToAssembly);
        }

        // Return the results of compilation.
        if (cr.Errors.Count > 0) {
            return null;
        } else {
            return cr.CompiledAssembly;
        }
    }
}