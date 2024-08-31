#!/bin/zsh

# get the directory of the root project. If running script in nested directory, change the number of `..`
ROOT_DIR=$(dirname $(dirname $(dirname $(realpath $0))))

echo $ROOT_DIR

 openapi-generator-cli generate \
     -g csharp \
     -i ./whoop-openapi.yaml \
     -c ./config.json \
     -o ./Testing \
     --global-property supportingFiles=false,apiTests=false,modelTests=false
