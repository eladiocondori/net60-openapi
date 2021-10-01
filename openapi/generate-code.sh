#!/bin/bash
echo "Generating code for OpenAPI specification"

echo Converting YAML spec to JSON
# swagger-cli bundle -t json openapi.yaml > openapi.json
docker run --rm -v ${PWD}:/wrk -w /wrk openapitools/openapi-generator-cli generate -i openapi.yaml -g openapi -o .generated

echo "Removing (potentially) existing files."
rm -r -f typescript
rm -r -f csharp

echo "Creating empty folders typescript, typescript\models and csharp for code-generation"
mkdir typescript
mkdir csharp
cd typescript
mkdir models
cd ..

echo "Creating typescript side code with orval"
echo "Orval commented out for now as could not find a working docker image for it atm"
#orval

echo "Creating csharp server code with nswag"
docker run --rm -v ${PWD}:/wrk countingup/nswag run wrk/net50_server.nswag /runtime:Net50 /variables:NameSpace=OpenApi.Example,SpecUrl=.generated/openapi.json

echo "Listing the generated code files"
ls -R typescript
ls -R csharp

echo "Copying generated files to correct place"

echo OpenAPI spec
cp .generated/openapi.json ../src/wwwroot/swagger/v1

echo OpenAPI server code
cp csharp/*.cs ../src/OpenApi/