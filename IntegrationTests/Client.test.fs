module IntegrationTests

open System
open System.IO
open Microsoft.Extensions.Configuration
open NUnit.Framework
open FsUnit

open Alex75.Tardigrade

let areEqual a b = //(a:byte[], b:byte[]) = // a b = 
    match (a:byte array).Length = (b:byte array).Length with
    | false -> false
    | true ->
        let rec check aa bb i =
            if i = a.Length-1 then true
            else
                match a.[i] = b.[i] with 
                | false -> false
                | true -> check aa bb (i+1)
        check a b 0



type ClientTest () =

    let apiKey = ConfigurationBuilder().AddUserSecrets("edb9da2d-7ae6-44f7-ab56-d14dab8a4657").Build();
    let apiName = apiKey.["API name"]
    let apiKey = apiKey.["API key"]
    

    let bucket = "test"

    let deleteFile fileName =
        () // not implemented

    [<Test>]
    member this.``Write and Read File``() =
        let fileName = sprintf "test_%d.bin" (DateTime.Now.Ticks)
        try
            let client = Client(Satellites.EuropeWest_1, apiName, apiKey)             
            let data = Array.create 10 (byte(1))

            // write
            client.WriteFile(bucket, fileName, data)

            // read
            let readedData = client.ReadFile(bucket, fileName)

            areEqual data readedData |> should be True

        finally
            //deleteLocalFile file
            deleteFile fileName

