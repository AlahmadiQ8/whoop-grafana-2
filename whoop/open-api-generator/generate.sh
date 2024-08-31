#!/bin/zsh

ROOT_DIR=$(dirname $(dirname $(dirname $(realpath $0))))

 openapi-generator-cli generate \
     -g csharp \
     -i ./whoop-openapi.yaml \
     -c ./config.json \
     -o "$ROOT_DIR/whoop" \
     --global-property apiTests=false,modelTests=false,apiDocs=false,modelDocs=false
