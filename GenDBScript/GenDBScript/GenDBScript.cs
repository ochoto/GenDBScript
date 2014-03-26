using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;

namespace GenDBScript {
    class GenDBScript {
        static void Main(string[] args) {

            Server s = new Server("CENTSQL18");
            foreach (Database dd in s.Databases) {
                System.Console.WriteLine("-- " + dd.Name);
            }
            System.Console.ReadLine();

            Database d = s.Databases["REPL_SIB"];
            //ScriptingOptions databaseOptions = new ScriptingOptions();
            //databaseOptions.Add(ScriptOption.WithDependencies);
            //StringCollection crtDb = d.Script(databaseOptions);

            //Transfer t = new Transfer(d);
            Transfer t = new Transfer(d);
            t.CopyData = false;              // Nos aseguramos de evitar los datos
            t.CopySchema = true;
            t.CopyAllObjects = true;
            //t.Options.ScriptDrops = true;   // Creamos script que sirva para recrear
            t.DropDestinationObjectsFirst = true;
            t.Options.LoginSid = true;
            t.Options.Indexes = true;
            t.Options.WithDependencies = true;
            String fSal = d.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".sql";
            StringCollection crtDb = t.ScriptTransfer();
            foreach (String crtS in crtDb) {
                System.Console.WriteLine(crtS);
            }

            System.Console.ReadLine();
        }
    }
}
