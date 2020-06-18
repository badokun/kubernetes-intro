# Kubernetes for Developers: Moving to the Cloud

Following along the course: <https://app.pluralsight.com/library/courses/kubernetes-developers-moving-cloud/table-of-contents>

```
az login

# Register namespaces
az provider register --namespace Microsoft.Network
az provider register --namespace Microsoft.Compute
az provider register --namespace Microsoft.OperationsManagement
az account list-locations -o table

# Create resource group
az group create --name kbs-demo --location japaneast

#create private registry in resource group
az acr create -g kbs-demo -l japaneast -n kbsregistry --sku Basic

#build docker image  
docker build -t kbsregistry.azurecr.io/kubernetes-intro/webapp:v5 .
docker tag <image-id> kbsregistry.azurecr.io/kubernetes-intro/webapp:v5

#upload image to registry
az acr login --name kbsregistry
docker push kbsregistry.azurecr.io/kubernetes-intro/webapp:v5

#create cluster (3 nodes) 
az aks create --resource-group=kbs-demo --name=demo-cluster --generate-ssh-keys --node-count=2 --node-vm-size=Standard_B2s

#grant AKS pull access to ACR
#Get the id of the service principal configured for AKS
CLIENT_ID=$(az aks show -g kbs-demo -n demo-cluster --query "servicePrincipalProfile.clientId" --output tsv)

#Get the ACR registry resource id
ACR_ID=$(az acr show -g kbs-demo -n kbsregistry --query "id" --output tsv)

#Create role assignment 
> Had to run this in a Console Window
az role assignment create --assignee $CLIENT_ID --scope $ACR_ID --role acrpull 

#configure kubectl 
az aks get-credentials --resource-group=kbs-demo --name=demo-cluster

#endable monitoring
az aks enable-addons -a monitoring -g kbs-demo -n demo-cluster

#create deployment
k create deployment webapp --image=kbsregistry.azurecr.io/kubernetes-intro/webapp:v5

#expose
k expose deployment webapp --type=LoadBalancer --port 80 --target-port 80

az acr build -t kubernetes-intro/webapp:v6 -r kbsregistry -f Dockerfile .
k create deployment webapp --image=kbsregistry.azurecr.io/kubernetes-intro/webapp:v6

#delete cluster 
az aks delete --name demo-cluster --resource-group kbs-demo 

#delete images  
az acr repository delete --name kbsregistry --image kubernetes-intro/webapp:v6
 
#remove resource group - will get rid of container registry and container images 
az group delete --name kbs-demo  
 
```

> Must follow format: `kbsregistry.azurecr.io/<namespace>/<image>:<tag>`
