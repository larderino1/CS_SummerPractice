# CS_SummerPractice
This repostory for our team project on summer practice
# Frameworks - Tools - Libraries
- ASP.NET Core
- Entity Framework Core
- Microsoft.Azure.Storage
- Microsoft.Extensions.Azure
- Azure.Blob.Storage
# Prerequisites
- Visual Studio 2019 Enterprise
- .NET Core SDK 3.1+
- Microsoft Azure Blob Storage
# How To Run
- Open solution in Visual Studio 2019 Enterprise
- Restore NuGet packages.
- Build the solution.
- Go to the Solution Explorer, right-click on its name and select the option "Set StartUp Projects…", then select "Backend_Category", "Backend_Product", and "Frontend" projects. "DbManager" project is used just as database migration tool. 
- Run the application.

To automate project's software development life cycle processes (build, test, deploy etc) we can create and configure custom workflows using YAML syntax, and save them as workflow files in the repository. At this point, we've added dotnet-core.yml file to do automated builds after each new commit into master.
