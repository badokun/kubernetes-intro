# kubernetes-intro
Following along with the Pluralsight Kubernetes Developer Path

For reference: https://app.pluralsight.com/paths/skills/using-kubernetes-as-a-developer

## Dashboard

In order to login to the dashboard you need to get the token. Execution this command

`kubectl describe secret -n kube-system | grep deployment -A 12`

The service will run on <http://localhost:8001/> after running `kubectl proxy`

## Kubernetes Examples

<https://github.com/kubernetes/examples>

## Troubleshooting Techniques

* k exec [pod-name] -it sh
* k logs -f [pod-name]
