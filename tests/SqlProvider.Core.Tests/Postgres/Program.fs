﻿// Learn more about F# at http://fsharp.org
//#r "../../bin/net461/FSharp.Data.SqlProvider.dll"
#if INTERACTIVE
#r @"C:\Program Files\dotnet\sdk\2.0.0\Microsoft\Microsoft.NET.Build.Extensions\net461\lib\netstandard.dll"
#r "../../../bin/netstandard2.0/FSharp.Data.SqlProvider.dll"
[<Literal>]
let resolutionPath = __SOURCE_DIRECTORY__ + "/temp"

#else
module Netstandard
[<Literal>]
let resolutionPath = "temp"

#endif

open System
open FSharp.Data.Sql

[<Literal>]
let connStr = "User ID=postgres;Host=localhost;Port=5432;Database=sqlprovider;Password=postgres"

type HR = SqlDataProvider<
            Common.DatabaseProviderTypes.POSTGRESQL, connStr,
            ResolutionPath = resolutionPath>

[<EntryPoint>]
let main argv =
    let runtimeConnectionString = connStr
    let ctx = HR.GetDataContext runtimeConnectionString
    let employeesFirstName = 
        query {
            for emp in ctx.Public.Employees do
            select emp.FirstName
        } |> Seq.head

    printfn "Hello %s!" employeesFirstName
    System.Threading.Thread.Sleep 2000
    0 // return an integer exit code
