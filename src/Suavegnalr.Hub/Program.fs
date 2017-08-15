namespace Suavegnalr

module Hubs =
    open Microsoft.AspNet.SignalR
    
    type ISendPings =
        abstract NewMessage : string -> unit

    type MessagesHub () =
        inherit Hub<ISendPings>()
        member this.SendLogPing = this.Clients.All.NewMessage
        
        
module Main =
    open Owin
    open Microsoft.Owin.Builder
    open Owin.Security.AesDataProtectorProvider
    open Microsoft.AspNet.SignalR
    open Hubs
    open Suave
    open Suave.Owin
    open Suave.Operators
    open Suave.Filters
    
    open System
    open System.Threading
    open Suave.Successful
    open System.Net
    
    [<EntryPoint>]
    let main [|port|] =

        let buildOwinApp (app : IAppBuilder)=
            //app.UseAesDataProtectorProvider()
            let config = new HubConfiguration(EnableDetailedErrors = true)        
            OwinExtensions.MapSignalR(app, config) |> ignore

        let owinApp =            
            let builder = new AppBuilder()            
            buildOwinApp(builder)
            builder.Build() |> OwinApp.ofAppFunc ""

        let hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<MessagesHub, ISendPings> ()

        let app = 
            
            choose [
                GET >=> path "/" >=> Files.browseFileHome "index.html"
                POST 
                    >=> path "/sndmsg" 
                    >=> request (fun req ->                             
                            match (req.formData "msg") with
                            | Choice1Of2 msg -> 
                                hubContext.Clients.All.NewMessage(msg)
                                OK "Sent"
                            | Choice2Of2 error -> 
                                printfn "Error %s" error
                                ServerErrors.INTERNAL_ERROR(error))
                owinApp
            ]
        
        
        let suaveConfig = { defaultConfig with bindings = [ HttpBinding.create HTTP IPAddress.Loopback (uint16 port) ]} 
        
        startWebServer suaveConfig app
        
        0 // return an integer exit code