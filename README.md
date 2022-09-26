# notes-app-dotnet

This is simple .NET 6 app that allows user to create notes belonging to certain categories.

## Your task

Your task is to prepare ansible playbook that will build & install notes-app as a service.

### 1. Install prerequisites
Install all required dependencies to build and host application (.NET 6):
https://learn.microsoft.com/en-us/dotnet/core/install/linux

### 2. Building/publishing .Net application
Fetch application source code on target machine and publish it to a directory that makes sense from FHS
(Filesystem Hierarchy Standard) perspective:

```shell
dotnet publish NotesAppDotnet/NotesAppDotnet.sln --configuration Release --output <destination_dir>
```

Command `dotnet publish` will download dependencies, build and package application to the output directory.

### 3. Create folder structure and configuration file

notes-app requires directory for SQL Lite database files (application will create them at startup), it's defined as
Settings.DataPath property in application configuration.

Example configuration file: appsettings.json (in application directory)

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

### 4. Make app run as a service

Dotnet application can be started in similiar way to Java Jars, in applciation directory execute:
`dotnet NotesAppDotnet.dll`

Service supports systemd notification protocol (Type=notify).


### 5. Verification

Add task to verify if application started and it's running properly.

### 6. Use exteral service to fetch configuration data
In Settings section there is subsection: "Tags", populate this list using data returned from gist service: https://gist.githubusercontent.com/m-j/58cda3e3be469191518c7d704ff336ba/raw/ba7ce0264109639ae8808e39f367d938cbfabad4/notes_app_dotnet_tags.txt

# Useful info about the notes-app

## Health endpoint
Basic healthcheck is implemented as /health endpoint.

## Swagger
Application exposes Swagger UI which can be accessed using <application_url>/swagger/index.html page from the browser.

## Publishing / listing notes
New notes could be published by POSTing json to /notes endpoint e.g.
```json
{ "content": "My first note" , "tag": "home" }
```
GET on same endpoint will list all published notes.

## Listing allowed tags
You can verify list of allowed tags by calling /tags endpoint.
