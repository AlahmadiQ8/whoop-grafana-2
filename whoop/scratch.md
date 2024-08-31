﻿# Notes 

## OpenApi Generator

```bash
openapi-generator-cli config-help -g csharp

openapi-generator-cli generate -g csharp -i ./whoop-openapi.yaml -c ./config.json -o ./Testing --global-property supportingFiles=false,apiTests=false,modelTests=false --dry-run 
```

```
Whoop.Sdk.sln
appveyor.yml
api/openapi.yaml
.gitignore
git_push.sh
README.md
```
