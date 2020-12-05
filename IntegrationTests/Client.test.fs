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
    let keyName = apiKey.["API name"]
    let keySecret = apiKey.["API secret"]    
    let bucket = "test"


    [<Test>]
    member this.``Write and Read File``() =
        let fileName = sprintf "test_%d.bin" (DateTime.Now.Ticks)

        let client = Client(Satellites.EuropeWest_1, keyName, keySecret)             
        let data = Array.create 10 (byte(1))

        // write
        client.WriteFile(bucket, fileName, data)

        // read
        let readedData = client.ReadFile(bucket, fileName)

        areEqual data readedData |> should be True


    [<Test>]
    member this.``Write and Delete``() =
        let fileName = sprintf "test_%d.bin" (DateTime.Now.Ticks)

        let client = Client(Satellites.EuropeWest_1, keyName, keySecret)             
        let data = Array.create 10 (byte(1))

        // write
        client.WriteFile(bucket, fileName, data)

        // delete
        client.DeleteFile(bucket, fileName)

        //(fun () -> client.ReadFile(bucket, fileName) |> ignore) |> should throw //(typeof<Exception>)
        try                 
            client.ReadFile(bucket, fileName) |> ignore
        with exc ->
            exc.ToString() |> should contain (sprintf "uplink: object not found (\"%s\")" fileName)          

            

