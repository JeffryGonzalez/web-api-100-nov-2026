# Kubernetes / Red Hat OpenShift Local

Run a small Kubernetes cluster *on your own machine* - using something like OpenShift Local, kind, k3d, or minikube - and create your local environment using the same configuration (Deployments, Services, ConfigMaps, etc.) that your app will use in all other environments.

Instead of a compose file that exists only for development, you develop against the real deployment manifests. Tools like Skaffold or Tilt can automate the build-deploy-watch loop so it feels closer to a normal inner loop.

## Advantages

- Closer to production (dev/prod parity) - you find configuration and networking problems on your laptop instead of in the staging environment.
- More resilient - Kubernetes restarts failed services on its own; self-healing comes for free.
- The manifests (deployment files) are the same as you'd use in other environments - no throwaway "dev-only" configuration to maintain.
- You learn the platform your app actually runs on.

## Disadvantages

- More intensive - a local cluster eats a *lot* of RAM and CPU, and the inner loop (build image, push, deploy, wait) is slower than hitting F5.
- Serious learning curve: you're learning Kubernetes *and* building an API at the same time.
- Debugging an app running inside a cluster is harder than debugging a local process.
- I don't think you can do this - realistically, corporate laptops and classroom machines often can't run a local cluster (resources, admin rights, virtualization settings).
