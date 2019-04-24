using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Windows.Forms;
namespace RansomBuilder
{
    class Builder
    {

        public static bool Build(string exe_name, string source, string settings, string icon_path)
        {
            CodeDomProvider Compiler = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters Parameters = new CompilerParameters();
            CompilerResults cResults = default(CompilerResults);
            Parameters.GenerateExecutable = true;
            Parameters.OutputAssembly = exe_name;
            Parameters.ReferencedAssemblies.Add("System.dll");
            Parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            Parameters.CompilerOptions = " /target:winexe";
            if (!string.IsNullOrEmpty(icon_path))
            {
                Parameters.CompilerOptions = " /win32icon:\"" + @icon_path + "\"";
            }
            Parameters.TreatWarningsAsErrors = false;
            cResults = Compiler.CompileAssemblyFromSource(Parameters, new string[] { source, settings });
            if (cResults.Errors.Count > 0)
            {
                
                MessageBox.Show("An error occurred while building, check settings(comma or etc.)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (cResults.Errors.Count == 0)
            {
                return true;
              
            }
            return true;
        }
    }
}
