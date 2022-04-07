# notes-app-dotnet

This is simple .NET 6 app that allows user to create notes belonging to certain categories. 

## Your task

Your task is to prepare ansible playbook that will clone this git repository and install 
this app as a service.

### 1. Cloning repository
Clone this repository (https://github.com/m-j/notes-app-dotnet)

### 2. Installing & building
Install all required required dependencies (.NET 6)

After .NET 6 is installed build an app:

```shell
dotnet build .\NotesAppDotnet.sln -c Release
```
Then copy generated binaries to folder that makes sense from FHS perspective

### 3. Create folder structure and configuration

App requires one folder that will contain SQL Lite database file. You'll have to create 
it and provided its path in configuration file. 

Configuration file has following format:

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "Kestrel": {
        "Endpoints": {
            "Http" : {
                "Protocols": "Http1AndHttp2AndHttp3",
                "Url": "http://*:8089"
            }
        }
    },
    "AllowedHosts": "*",
    "Settings" : {
        "DataPath" : "C:\\tmp\\notes_app",
        "Tags" : ["shopping", "work", "home"]
    }
}

```

In Settings section there is subsection: "Tags". 
Generate this config using jinja2 template engine and populate "Tags" section with
list of items downloaded from this gist: https://gist.github.com/m-j/58cda3e3be469191518c7d704ff336ba

(You can click on Raw button in order to get plain txt)
### 4. Make app run as a service



# Useful info

# Running dll
You can start .NET app in similar to starting javas JAR:

`dotnet NotesAppDotnet.dll`

## Choosing config file to use
App can have multiple appsettings files in directory. 
To use specific one you can set environment variable:

```shell
ASPNETCORE_ENVIRONMENT=Release
```

Ths will make app use: `appsettings.Release.json`


