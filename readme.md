# Suave + SignalR + Azure Web App

### Empecé

Con el código de este ejemplo:
- [SignalRDemo](https://github.com/SuaveIO/suave/tree/master/examples/SignalRDemo)

Seguí estas instrucciones:

- [Continuous Deployment to Azure App Service](https://docs.microsoft.com/en-us/azure/app-service-web/app-service-continuous-deployment) 

- [Deploying Suave to Azure App Service](https://docs.microsoft.com/en-us/azure/app-service-web/app-service-continuous-deployment)

- El script de FAKE [completo](https://github.com/isaacabraham/fsharp-demonstrator/blob/master/build.fsx)


### Pero
- El `startWebServerAsync` [no me funcionó en Azure](https://github.com/nicolocodev/suavegnalr/commit/0b3f03fea1fc361ed24b1dbcc64262bbf596b39b)
- Recibía la excepción: `The data protection operation was unsuccessful. This may have been caused by not having the user profile loaded for the current thread's user context, which may be the case when the thread is impersonating.` Encontré la solución [aquí](http://csharp.wekeepcoding.com/article/13024406/SignalR+negotiate+fails+(Bad+Gateway)) y [en SO](https://stackoverflow.com/a/41748679/1229323). Y [funciona](https://github.com/nicolocodev/suavegnalr/commit/60931128587244ce03aa8d376fc4f5d02cb35f0a#diff-a688314c30f2b1c711d5c71db911c57b)
