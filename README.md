# Overview

In sprint #3, I wrote a cryptography class for encrypting/decrypting text. To demo this project, I built it into a console application. (You can find that project and its documentation here: [applied-programming-encrypt-decrypt](https://github.com/ScratchyPDX/applied-programming-encrypt-decrypt)). 

However, it occurred to me that, in today's business environment, a console application would not fly as it was not user friendly or portable. So, I looked at writing a frontend project that would meet the following goals:
- Must be an installable application
- Must have the ability to run on multiple devices/OSs
- Must use the Crypto class from sprint #3 as a DLL
- Must continue support of reading/writing encrypted text from/to a file

Unlike a web application or website written using Javascript, HTML and/or Angular, .NET Maui apps are install and run directly on the host device, and are supported Android, iOS, Windows, Linux and MacOS. When properly configured, a single code base can be built and deployed to any or all of these devices/operating systems without the need for device/OS-specific program code.

[Software Demo Video](http://youtube.link.goes.here)

# Development Environment

While this project was initially started using Visual Studio Code, I found Visual Studio 2022 Community Edition (17.6 or greater) was easier to manage debugging, and the application was more responsive during a debug session. 

.NET Maui requires installation of .NET v8 or greater. So, while the Visual Studio 17.6 installer (for MacOS) does not have an option to install this version of .NET, manual installation is required and supported. For Windows, the Visual Studio 2022 Community 17.8 installer supports .NET 8 out of the box; no manual installation steps are required.

This project also depends on a class library (DLL) for cryptography. It is written in C# and .NET 8. The project code and documentation can be found on Github here: [applied-programming-crypto-library](https://github.com/ScratchyPDX/applied-programming-crypto-library)

.NET Maui uses XAML for describing the layout of the application screen(s) and C# for code behind event handling. .NET Maui projects can implement enhanced UI/UX controls and behaviors by including .NET Maui Community Toolkit, which is an open source project.

# Useful Websites

- [What is .NET Maui](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui?view=net-maui-8.0)
- [Download .NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Build Your First .NET Maui App](https://learn.microsoft.com/en-us/dotnet/maui/get-started/first-app?view=net-maui-8.0&tabs=vswin&pivots=devices-maccatalyst)
- [.NET Mail Community Toolkit Documentation](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/)
- [.NET Maui Community Toolkit on Github](https://github.com/CommunityToolkit/Maui)

# Future Work

- While .NET Maui supports iOS and Android, additional installation/configuration on the build machine is required. Due to time constraints, I did not complete this, but if development were to continue, inclusion of iOS and Android deployments would need to be completed.

- Instead of including the Crypto class library directly in the project, further steps would import the Crypto library from NuGet.

- I'm not a UI/UX designer, so involving someone who is more skilled in this area would be nice. While the application is functional, it might not be layed out in the most user-friendly configuration.