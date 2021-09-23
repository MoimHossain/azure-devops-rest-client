# azure-devops-rest-client

A C# REST client for Azure DevOps and it's nuances 


## Run with Docker

Create a project:

```
docker run --rm moimhossain/pat-test:v1 `
    -u "https://dev.azure.com/<ORG-NAME>/" `
    -c "YOUR PAT" `
    -p "test-project" `
    -v "Create"
```



Delete a project:

```
docker run --rm moimhossain/pat-test:v1 `
    -u "https://dev.azure.com/<ORG-NAME>/" `
    -c "YOUR PAT" `
    -p "test-project" `
    -v "Delete"
```
