using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Text;

namespace SoftuniHTTPServer.MvcFramework.ViewEngine
{
    public class SHSViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            string csharpCode = GenerateCSharpFromTemplate(templateCode);
            IView executableObject = GenerateExecutableCode(csharpCode, viewModel);
            string html = executableObject.ExecuteTemplate(viewModel);
            return html;
        }

        private string GenerateCSharpFromTemplate(string templateCode)
        {
            string csSharpCode = @"
                using System;
                using System.Text;
                using System.Linq;
                using System.Collections.Generic;
                using SoftuniHTTPServer.MvcFramework.ViewEngine;

                namespace ViewNamespace
                {
                    public class ViewClass : IView
                    {
                        public string ExecuteTemplate(object viewModel)
                        {
                            var html = new StringBuilder();

                            " + GetMethodBody(templateCode) + @"
                            
                            return html.ToString();
                        }
                    }
                }";


            return csSharpCode;
        }

        private string GetMethodBody(string templateCode)
        {
            StringBuilder csharpCode = new StringBuilder();
            StringReader sr = new StringReader(templateCode);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.TrimStart().StartsWith("@"))
                {
                    var atSignLocation = line.IndexOf("@");
                    line = line.Remove(atSignLocation, 1);
                    csharpCode.AppendLine(line);
                }
                else if (line.TrimStart().StartsWith("{") || 
                    line.TrimStart().StartsWith("}"))
                {
                    csharpCode.AppendLine(line);
                }

                csharpCode.AppendLine($"html.AppendLine(@\"{line.Replace("\"", "\"\"")}\");");
            }
            return csharpCode.ToString();
        }

        private IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            // creates assembly with name ViewAssembly
            var compileResult = CSharpCompilation.Create("ViewAssembly")
                // adds options class with OutputKind which is dll
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                // adds references to the assembly (dynamically adds the path to those files)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            if (viewModel != null)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            var libraries = Assembly.Load(
                new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }

            compileResult = compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                EmitResult emitResult = compileResult.Emit(memoryStream);

                if (!emitResult.Success)
                {
                    // Compile errors
                    return new ErrorView(emitResult
                        .Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error)
                        .Select(d => d.GetMessage()), csharpCode);
                }

                try
                {
                    // we do this because the stream rights to some point
                    // in the stram and when we want to read form it 
                    // it will start from the last point of. That is
                    // why we neet to specify from which point we want
                    // to read from he stram
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var byteAssembly = memoryStream.ToArray();
                    var assembly = Assembly.Load(byteAssembly);
                    var viewType = assembly.GetType("ViewNamespace.ViewClass");
                    var instance = Activator.CreateInstance(viewType);
                    return (instance as IView)!;
                }
                catch (Exception ex)
                {
                    return new ErrorView(new List<string> { ex.ToString() }, csharpCode);
                }
            }
        }
    }
}
