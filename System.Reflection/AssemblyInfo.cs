using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
    /// <summary>
    /// The AssemblyInfo class contains information about an <see cref="Reflection.Assembly"/> like its versions, copyright text, title, configuration, and more.
    /// <note type="security">
    /// The caller must have full trust permission due to its internal usage of the <see cref="FileVersionInfo"/> class.
    /// </note>
    /// </summary>
    /// <example>
    /// The following example shows how one can use the AssemblyInfo class.
    /// <code>
    /// [assembly: AssemblyTitle("AssemblyInfoExample")]
    /// [assembly: AssemblyDescription("My description.")]
    /// [assembly: AssemblyInformationalVersion("Hamburgers")]
    /// [assembly: AssemblyCopyright("2142")]
    /// [assembly: AssemblyConfiguration("Debug")]
    /// public static class Program
    /// {
    ///     public static void Main(string[] args)
    ///     {
    ///         var info = AssemblyInfo.EntryAssembly;
    ///         Console.WriteLine($"Title: {info.Title}");
    ///         Console.WriteLine($"Description: {info.Description}");
    ///         Console.WriteLine($"Version: {info.InformationalVersion}");
    ///         Console.WriteLine($"Copyright: {info.Copyright}");
    ///         Console.WriteLine($"Configuration: {info.Configuration}");
    ///         Console.WriteLine($"Path: {info.AssemblyPath}");
    ///         Console.ReadKey(true);
    ///     }
    /// }
    /// Output:
    ///     Title: AssemblyInfoExample
    ///     Description: My description.
    ///     Version: Hamburgers
    ///     Copyright: 2142
    ///     Configuration: Debug
    ///     Path: C:\some\path\to\AssemblyInfoExample.dll
    /// </code>
    /// </example>
    /// <threadsafety static="true" instance="true"/>
    public class AssemblyInfo
    {
        /// <summary>
        /// Gets the AssemblyInfo of the entry assembly.
        /// </summary>
        public static AssemblyInfo EntryAssembly => new AssemblyInfo(Assembly.GetEntryAssembly());

        /// <summary>
        /// Gets the AssemblyInfo of the calling assembly.
        /// </summary>
        public static AssemblyInfo CallingAssembly => new AssemblyInfo(Assembly.GetCallingAssembly());

        /// <summary>
        /// Gets the <see cref="System.Reflection.Assembly"/> object used to generate this AssemblyInfo.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the title of the assembly.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the product name of the assembly.
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// Gets the description of the assembly.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the configuration of the assembly.
        /// </summary>
        public string Configuration { get; }

        /// <summary>
        /// Gets the company name that authored the assembly.
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// Gets the assembly's copyright.
        /// </summary>
        public string Copyright { get; }

        /// <summary>
        /// Gets the assembly's trademark.
        /// </summary>
        public string Trademark { get; }

        /// <summary>
        /// Gets the culture of the assembly.
        /// </summary>
        public string Culture { get; }

        /// <summary>
        /// Gets whether or not the assembly is visible to COM.
        /// </summary>
        public bool ComVisible { get; }

        /// <summary>
        /// Gets the GUID of the assembly.
        /// </summary>
        public string Guid { get; }

        /// <summary>
        /// Gets the assembly version of the assembly.
        /// </summary>
        public string AssemblyVersion { get; }

        /// <summary>
        /// Gets the file version of the assembly.
        /// </summary>
        public string FileVersion { get; }

        /// <summary>
        /// Gets the informational version of the assembly.
        /// </summary>
        public string InformationalVersion { get; }

        /// <summary>
        /// Gets the filepath to the assembly.
        /// </summary>
        public string AssemblyPath { get; }

        /// <summary>
        /// Gets the directory path that the assembly resides in.
        /// </summary>
        public string AssemblyDirectory { get; }

        /// <summary>
        /// Creates an AssemblyInfo object from the specified <see cref="System.Reflection.Assembly"/>.
        /// </summary>
        /// <param name="assembly"></param>
        /// <exception cref="SecurityException">Thrown when the immediate caller does not have full trust permission.</exception>
        public AssemblyInfo(Assembly assembly)
        {
            Assembly = assembly;
            AssemblyVersion = Assembly.GetName().Version.ToString();
            AssemblyPath = GetAssemblyPath(Assembly);
            AssemblyDirectory = GetAssemblyDirectory(Assembly);
            var fvi = FileVersionInfo.GetVersionInfo(AssemblyPath);
            FileVersion = fvi.FileVersion;
            InformationalVersion = fvi.ProductVersion;

            foreach (var attr in Assembly.GetCustomAttributes())
            {
                switch (attr)
                {
                    case AssemblyProductAttribute a:
                        ProductName = a.Product;
                        break;
                    case AssemblyConfigurationAttribute a:
                        Configuration = a.Configuration;
                        break;
                    case AssemblyDescriptionAttribute a:
                        Description = a.Description;
                        break;
                    case AssemblyTrademarkAttribute a:
                        Trademark = a.Trademark;
                        break;
                    case AssemblyTitleAttribute a:
                        Title = a.Title;
                        break;
                    case AssemblyCompanyAttribute a:
                        Company = a.Company;
                        break;
                    case AssemblyCultureAttribute a:
                        Culture = a.Culture;
                        break;
                    case ComVisibleAttribute a:
                        ComVisible = a.Value;
                        break;
                    case GuidAttribute a:
                        Guid = a.Value;
                        break;
                    case AssemblyCopyrightAttribute a:
                        Copyright = a.Copyright;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="AssemblyInfo"/> generated from the specified <see cref="System.Reflection.Assembly"/>.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static AssemblyInfo FromAssembly(Assembly assembly)
        {
            return new AssemblyInfo(assembly);
        }

        /// <summary>
        /// Gets the <see cref="AssemblyInfo"/> generated from the specified type's <see cref="System.Reflection.Assembly"/>.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public static AssemblyInfo FromType<TType>()
        {
            return new AssemblyInfo(typeof(TType).Assembly);
        }

        /// <summary>
        /// Gets the filepath to this assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static string GetAssemblyPath(Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
        }

        /// <summary>
        /// Gets the directory that this assembly resides in.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static string GetAssemblyDirectory(Assembly assembly)
        {
            return Path.GetDirectoryName(GetAssemblyPath(assembly));
        }
    }
}
