# Notes

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

## Adding cosmos necessary permissions

https://techcommunity.microsoft.com/t5/azure-architecture-blog/configure-rbac-for-cosmos-db-with-managed-identity-instead-of/ba-p/3056638

```powershell
az cosmosdb sql role definition create -a "whoop" -g "whoop-grafana-v2" -b @role-definition-ro.json
az cosmosdb sql role definition create -a "whoop" -g "whoop-grafana-v2" -b @role-definition-rw.json

az cosmosdb sql role definition list --account-name "whoop" -g "whoop-grafana-v2"
az cosmosdb sql role assignment create -a "whoop" -g "whoop-grafana-v2" -s "/" -p "68b5c738-954a-479d-b660-244740a04886" -d "4fecc03e-8f97-4b2b-b239-6008aa6c82f1"


az cosmosdb sql role assignment create -a "whoop" -g "whoop-grafana-v2" -s "/" -p "f739f28b-b823-4b3f-b60b-49b56b1eea9e" -d "4fecc03e-8f97-4b2b-b239-6008aa6c82f1"
```
