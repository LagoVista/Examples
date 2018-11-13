# Simple example to show how to create a Serverless function to be uploaded to OpenFAAS

In most cases you will need to do async processing on your PEM, so the console app uses C# 7.1 so the main method can return a Task<> rather
than a void or int.  This isn't supported in the default OpenFAAS templates so we just packaged up everything in a self contained solution.

Special note, it looks like the primary executable *must* be named root.dll, it doesn't use the environment variable fprocess.
