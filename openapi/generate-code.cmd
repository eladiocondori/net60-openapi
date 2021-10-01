echo off

echo Converting YAML to JSON
@REM cmd /c swagger-cli bundle -t json openapi.yaml > openapi.json
cmd /c docker run --rm -v "%cd%:/wrk" -w /wrk openapitools/openapi-generator-cli generate -i openapi.yaml -g openapi -o .generated

echo Generating code for OpenAPI specification

echo Removing (potentially) existing files.
rmdir /s /q typescript
rmdir /s /q csharp

echo Creating empty folders typescript, typescript\models and csharp for code-generation
mkdir typescript
mkdir csharp
cd typescript
mkdir models
cd ..

echo Creating typescript (typescript) code with orval
cmd /c orval

echo Creating C# server code with nswag
cmd /c nswag run net50_server.nswag /runtime:Net50 /variables:NameSpace=OpenApi.Example,SpecUrl=.generated/openapi.json

echo Listing the generated code files
dir /s /b typescript
dir /s /b csharp
cd ..

echo "Copying generated files to correct place"

echo OpenAPI spec
copy .generated\openapi.json ..\src\wwroot\swagger\v1 /Y

echo OpenAPI server code
copy csharp\*.cs ..\src\OpenApi\ /Y

goto DONE

:DONE
echo on