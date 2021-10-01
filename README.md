# net60 minimal api with generated openAPI code

This repository contains net60 minimal api with generated code from OpenAPI spec.

## Generating code

Modify the `openapi/openapi.yaml` and run `generate-code.sh / generate-code.cmd` which generates the c# code (net50 as there is no support for net60 yet) using [NSwag](https://github.com/RicoSuter/NSwag).

## Running the code

```shell
cd src
dotnet run --urls=http://localhost:10000
```

Open browser to `http://localhost:10000` and visit `http://localhost:10000/swagger` for the swagger UI.