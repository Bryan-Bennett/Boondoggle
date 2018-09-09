# Boondoggle .NET Namespace Extension Libraries

The Boondoggle .NET Namespace Extension Libraries contain types that extend the codebases of the various .NET namespaces.  Each type uses a .NET namespace like System.Reflection
instead of something like Boondoggle.Reflection or Boondoggle.System.Reflection.  This makes using Boondoggle types easier on developers.  However, it is unconventional -- by convention,
Boondoggle should be using namespaces in the Boondoggle.* format.  With that said, the decision to use .NET namespaces was based on how we wanted to present "namespace extensions".

***Note: If any future types implemented by Microsoft clashes with Boondoggle types, the Boondoggle type will be renamed or removed.  The frequency of this happening should be very rare but it is a possibility due
to the unconventional namespace style Boondoggle uses.***

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

* [Sandcastle](https://github.com/EWSoftware/SHFB) - Download the latest version of Sandcastle and follow the installation instructions.  Ensure that you install most optional content concerning MAML and Visual Studio integration.  If you do not wish to download and install Sandcastle, simply unload the Boondoggle.Documentation project in your local solution.

### Development Workflow

*coming soon*

## Built With

* [Visual Studio 2017 Community](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2017) - IDE
* [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) - .NET implementation
* [NuGet](https://www.nuget.org/) - Dependency Management
* [Sandcastle](https://github.com/EWSoftware/SHFB) - Documentation generation utility

## Versioning

We use [Semantic Versioning](http://semver.org/) for versioning.

## Authors

* [Bryan Bennett](https://github.com/Bryan-Bennett) - *Initial work and project owner*

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details

## Acknowledgments

*coming soon*
