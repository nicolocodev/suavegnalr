open Microsoft.AspNet.SignalR.Client

[<EntryPoint>]
let main argv = 

    #if DEBUG
    let connection = new HubConnection("http://localhost:8080/")
    #else
    let connection = new HubConnection("")
    #endif

    let hub = connection.CreateHubProxy("MessagesHub")

    let ev = hub.On<string>("NewMessage", fun msg -> printfn "New message: %s" msg)
    
    connection.Start() |> Async.AwaitTask |> Async.RunSynchronously

    printfn "...exit?"

    System.Console.ReadLine() |> ignore

    connection.Dispose()

    ev.Dispose()

    0 // return an integer exit code
