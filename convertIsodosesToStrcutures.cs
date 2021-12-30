using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.1.0.1")]
[assembly: AssemblyFileVersion("1.1.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
[assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        const string SCRIPT_NAME = "convertIsodosesToStructures";
        public Script()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.

            if (context.PlanSetup == null)
            {
                MessageBox.Show("Please load a plan before running this script.", SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (context.PlanSetup.Dose == null)
            {
                MessageBox.Show("Please complete dose calculation  before running this script.", SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            StructureSet ss = context.StructureSet;
            Dose dose = context.PlanSetup.Dose;
            string logtext = "";

            // enable writing with this script.
            context.Patient.BeginModifications();

            foreach (Isodose isodose in context.PlanSetup.Dose.Isodoses)
            {
                string isoStructureName = "";
                isoStructureName = context.PlanSetup.Id + "_"
                    + string.Format("{0:f3}", Math.Round(context.PlanSetup.TotalDose.Dose * isodose.Level.Dose / 100.0, 3)) + "Gy";
                //isoStructureName = context.PlanSetup.Id + "_" + isodose.Level.ValueAsString + isodose.Level.UnitAsString;
                int nameLength = isoStructureName.Length;
                string isoStructureId = "";
                if (isoStructureName.Length > 16)
                {
                    isoStructureId = isoStructureName.Substring(0, 16);
                }
                else
                {
                    isoStructureId = isoStructureName;
                }

                if (ss.CanAddStructure("DOSE_REGION", isoStructureId) == true)
                {
                    Structure newIsoStructure = ss.Structures.FirstOrDefault(x => x.Id == isoStructureId);
                    if (newIsoStructure == null)
                    {
                        newIsoStructure = ss.AddStructure("DOSE_REGION", isoStructureId);
                    }

                    newIsoStructure.ConvertDoseLevelToStructure(dose, isodose.Level);

                    // this code from v16.1
                    newIsoStructure.Color = isodose.Color;

                    logtext += isoStructureName + "\n";
                }
            }
            MessageBox.Show(logtext + "\nDone.", SCRIPT_NAME);
        }
    }
}
