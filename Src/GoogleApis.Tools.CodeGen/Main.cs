using System;
using System.CodeDom.Compiler;
using System.IO;

using Google.Apis.Discovery;

namespace Google.Apis.Tools.CodeGen
{
	public class MainClass
	{
		/// <summary>
		/// Creates an Strongly Typed Client API for a given service.
		/// 
		/// This Tool is meant to serve as an example of how you can build your own libraries to serve your needs.
		/// 
		/// </summary>
		/// <param name="args">
		/// A <see cref="System.String[]"/>
		/// </param>
		/// <example>
		/// execute as follows: GoogleApis.Tools.CodeGen.exe buzz
		/// 
		/// creates the following api:
		/// 
		/// var client = new BuzzClient();
		/// client
		/// 	.WithAuthentication()
		/// 	.Activties.
		/// 	.List()
		/// 
		/// client
		/// 	.WithAuthentication()
		/// 	.Activties.
		/// 	.Delete()
		/// 
		/// 
		/// </example>
		public static void Main (string[] args)
		{
			if(args.Length == 0) {
				// Show the help
				return;	
			}
			
			var version = "v1";
			var serviceName = "buzz";
			var clientNamespace = "Google.Apis.Clients";
			var language = "CSharp";
			var outputFile = "";
			
			if(args.Length > 0)
			{
				serviceName = args[0];
				
				if(args.Length == 2) 
					version = args[1];
			}
				
			
			// Set up how discovery works.
			var webfetcher = new WebDiscoveryDevice { DiscoveryUri = new Uri("http://www.googleapis.com/discovery/0.1/describe?api=" + serviceName)};
			
			var discovery = new DiscoveryService(webfetcher);
			// Build the service based on discovery information.
			var service = discovery.GetService(version);
			
			var generator = new CodeGen(service, clientNamespace);
			
			var provider = CodeDomProvider.CreateProvider(language);
			
			using (StreamWriter sw = new StreamWriter(outputFile, false))
		    {
		        IndentedTextWriter tw = new IndentedTextWriter(sw, "  ");
		
		        // Generate source code using the code provider.
		        
				provider.GenerateCodeFromCompileUnit(generator.Generate(), tw,
		            new CodeGeneratorOptions());
		
		        // Close the output file.
		        tw.Close();
		    }
			
		}
	}
}
