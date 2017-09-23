using System.Threading.Tasks;
using NConsole;
using NSwag.SwaggerGeneration;

namespace NSwag.Commands
{
    [Command(Name = "list-types", Description = "List all types for the given assembly and settings.")]
    public abstract class ListTypesCommandBase : AssemblyOutputCommandBase<AssemblyTypeToSwaggerGeneratorBase>
    {
        protected ListTypesCommandBase(IAssemblySettings settings) 
            : base(settings)
        {
        }

        [Argument(Name = "File", IsRequired = false, Description = "The nswag.json configuration file path.")]
        public string File { get; set; }

        [Argument(Name = "Assembly", IsRequired = false, Description = "The path to the Web API .NET assembly.")]
        public string[] AssemblyPaths
        {
            get { return Settings.AssemblySettings.AssemblyPaths; }
            set { Settings.AssemblySettings.AssemblyPaths = value; }
        }

        public override async Task<object> RunAsync(CommandLineProcessor processor, IConsoleHost host)
        {
            return await Task.Run(async () =>
            {
                var generator = await CreateGeneratorAsync();
                var classNames = generator.GetExportedClassNames();

                host.WriteMessage("\r\n");
                foreach (var className in classNames)
                    host.WriteMessage(className + "\r\n");
                host.WriteMessage("\r\n");

                return classNames;
            });
        }
    }
}