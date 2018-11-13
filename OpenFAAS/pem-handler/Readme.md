# Simple example to show how to create a Serverless function to be uploaded to OpenFAAS

In most cases you will need to do async processing on your PEM, so the console app uses C# 7.1 so the main method can return a Task<> rather
than a void or int.  This isn't supported in the default OpenFAAS templates so we just packaged up everything in a self contained solution.

Special note, it looks like the primary executable *must* be named root.dll, it doesn't use the environment variable fprocess.

### Required Customization

In Publish.ps1 change the name of the docker image from `bytemaster/pem-hander` to the name of your image you want to deploy.

In ./pem-handler.yml change `http://openfaas.iothost.net:8080` to the address of your server and the functions to follow the specifications at [https://docs.openfaas.com/reference/yaml/](https://docs.openfaas.com/reference/yaml/).


_note_ the powershell script assumes that the function has already been published, to publish it the first time change 

```
faas-cli deploy --replace --update=false -f pem-handler.yml
```

to 

```
faas-cli deploy -f pem-handler.yml
```
