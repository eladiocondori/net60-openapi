# .NET 6 minimal API with generated openAPI code

This repository contains [.NET 6 minimal API](https://dotnetcoretutorials.com/2021/07/16/building-minimal-apis-in-net-6/) project with generated code from OpenAPI spec.

This project is used as a study to check the tooling around the .NET 6 minimalist approach combined with OpenAPI generated code and thus brings the usage a step closer to an actual enterprise level software.

## OpenAPI

In a true outside-in API thinking the OpenAPI specification (contract) is the source of truth for the API model. Thus it should be lifted outside of any single implementation (client/server) and have it's own versioning and life-cycle.

This contract is then used to generate the needed server side and client side code in the actual implementation of this contract.

This also supports the shift-left paradigm as the design of the domain is the first thing that happens and the documentation that is used for the domain modelling is not a one-time use tool.

[read more about this](https://swagger.io/blog/api-design/design-first-or-code-first-api-development/)

Tooling is important part around the OpenAPI ecosystem and following chapter covers few useful tools.

### Code generators

There are several different code generators available for OpenAPI specification. Few of the more popular ones are:

1. [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator)
2. [Swagger-CodeGen](https://github.com/swagger-api/swagger-codegen)
3. [NSwag](https://github.com/RicoSuter/NSwag)

For additional tooling around OpenAPI check [OpenAPI tooling](https://openapi.tools/).

#### Server-side

Any of these can be used in generating the initial server stub code, but the bigger question comes after that. How can we use the same generation tool to update the already implemented code with changes in the OpenAPI contract?

The first two use the more direct way of generating the server stub and create the .NET Core API controllers directly from the contract. This means that re-generating the controllers in later stage would result in losing the code that is already implemented. Thus this approach can only be used for generating the initial stub. All later updates will require manual work.

NSwag however has chosen a different approach. The code it auto generates is accessed via interfaces which means that the code can be regenerated as the controllers implement the interfaces and thus do not result in code loss.

Another related thing for this is the conversion of the spec from YAML to JSON format. Both are accepted formats, but .NET Core has built-in content-type support only for JSON (when serving static files from `wwwroot`) and thus the conversion is made at this point to make things easier.

#### Client-side

In similar manner pretty much any code generation tool can be used for generating the client-side code. The one that was selected for this project is [Orval](https://orval.dev/overview). The biggest reason why it was selected is it's ability to generate [React query](https://orval.dev/guides/react-query) out of the box.

Even though I'm not using this client-side code (yet) in this project I still chose to include it in the code generation.

### Test generators

(Under testing, no-pun intended).

Portman seems promising for generating the Postman collections from OpenAPI spec.

```bash
portman -l openapi.yaml -c portman-config.json -o example.postman.json --runNewman
```

### Spec comparison

When adding governance as part of the build pipeline having automated tool for comparing the changes between the OpenAPI spec versions will come in handy. 

The most important part of the check is to ensure that backwards compatibility doesn't break.

One tool that seems to do just this is [OpenAPI-Diff](https://github.com/OpenAPITools/openapi-diff).

```
git show master:openapi/openapi.yaml > openapi.old.yaml

docker run --rm -t \
  -v $(pwd):/specs:ro \
  openapitools/openapi-diff:latest /specs/openapi.old.yaml /specs/openapi.yaml
```

## Running the app

```bash
cd src
dotnet run --urls=http://localhost:10000
```

Open browser to `http://localhost:10000` and visit `http://localhost:10000/swagger` for the swagger UI.

## Modifying the spec and re-generating the code

Modify the `openapi/openapi.yaml` and run `generate-code.sh / generate-code.cmd` which generates the c# code (net50 as there is no support for net60 yet) using [NSwag](https://github.com/RicoSuter/NSwag) and copies both the generated code under the app folder `src/OpenApi` as well as the converted spec (yaml to json) under `src/wwwroot/swagger/v1`.

Next apply the changes you've actually done to the implementation of the spec. Do this in the classes that are under `src/Domain` folder.

## Docker

In case you prefer docker then please do use it.

```bash
docker build -t net60api .
docker run --rm -d -p 10000:8040 --name myapp net60api MinApi.dll
curl http://localhost:10000
# Hello World! I'm example from generated OpenAPI code. Visit /swagger for the spec UI.%
```
