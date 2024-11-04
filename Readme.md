# OpenDDS - C# Dotnet implementation

OpenDDS is an open sourced implementation of ***Data Distribution Service***. Data distribution Service is a modern approach to handling data transmission in a real-time commericial environment.
Read more about it from the [official source](https://opendds.org/)

## Key Documentation and References
1. [Developer Documentation](https://opendds.readthedocs.io/en/latest-release/)
2. [QuickStart Guide on All platforms](https://opendds.readthedocs.io/en/latest-release/devguide/quickstart/index.html)
3. [Going through sample project to understand how it works](https://opendds.readthedocs.io/en/latest-release/devguide/getting_started.html)
4. [Understanding custom types in OpenDDS](https://opendds.readthedocs.io/en/latest-release/devguide/opendds_idl.html)
5. [OpenDDS Github Repo](https://github.com/OpenDDS/OpenDDS)
6. [OpenDDSharp - the library we are using](https://www.openddsharp.com/)
7. [OpenDDSharp Getting Started - what is implemented](https://www.openddsharp.com/#getting-started)

## Project Contents
In the Git bundle, it should contain a sample project written in Windows Environment using dotnet 6.0 Solution implementing **OpenDDS version 3.29.1** with custom types .

### Software Requirements
1. Windows System
2. DotNet6.0 or higher
3. OpenDDS version 3.29.1 or higher (without breaking changes)

### Pre-requisite
Please note that in order to run the contents you must have **OpenDDS environments properly set up - typically through the `setenv.cmd` that is generated from OpenDDS**. 

### Building and testing the project
Ensure you have your environments properly set, follow the instructions above to do so.
Followin the guide from OpenDDSharp, you can compile according to its instructions. At the time of writing this is how its done for the various systems

Publisher
    
    dotnet build HelloWorldPublisher/HelloWorldPublisher.csproj --configuration <Release|Debug> --runtime <runtime_identifier> --no-self-contained

Subscriber
    
    dotnet build HelloWorldSubscriber/HelloWorldSubscriber.csproj --configuration <Release|Debug> --runtime <runtime_identifier> --no-self-contained

***NOTE***
The implemented runtime identifiers are:
- win-x64
- win-x86
- linux-x64
- osx-x64

Additional Notes and solutions can be found in [OpenDDSharp Getting Started](https://www.openddsharp.com/#getting-started)

Make sure to compile both projects. If all fields are set properly, you should compile successfully.

### Creating and building your own custom type
IDL is the custom language OpenDDS uses to generate types on the fly. 
The file used can be found under `Messenger\IDL` folder and in `Messenger.idl`.

### Conclusion
This initial progress shows how to isolate and utilize OpenDDS with a C# interface including how to use a custom class and type written. More exploratory work can be done with complexity that is introduced with doing the project in a production ready, resilent and scalability.  