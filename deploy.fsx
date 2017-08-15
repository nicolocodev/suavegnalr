#I @"packages/build/FAKE/tools/"
#r @"FakeLib.dll"

open Fake
open Fake.Azure
open System

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let solutionFile = "src/Suavegnalr.Hub/Suavegnalr.Hub.fsproj"

Target "BuildHub" (fun _ ->
    solutionFile
    |> MSBuildHelper.build (fun defaults ->
        { defaults with        
            Verbosity = Some Minimal
            Targets = [ "Build" ]
            Properties = [ "Configuration", "Release"
                           "OutputPath", Kudu.deploymentTemp ] })
    |> ignore)

Target "Deploy" Kudu.kuduSync

"BuildHub"
==> "Deploy"

RunTargetOrDefault "Deploy"