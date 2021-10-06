#!/bin/bash
echo "Comparing OpenAPI specification with previous version"

echo "Getting previous version of the specification"
git show master:openapi/openapi.yaml > openapi.old.yaml

docker run --rm -t \
  -v $(pwd):/specs:ro \
  openapitools/openapi-diff:latest /specs/openapi.old.yaml /specs/openapi.yaml