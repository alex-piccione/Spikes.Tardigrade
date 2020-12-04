module IntegrationTests

open System
open System.IO
open Microsoft.Extensions.Configuration
open NUnit.Framework

open Alex75.Tardigrade


type ClientTest () =

    let apiKey = ConfigurationBuilder().AddUserSecrets("edb9da2d-7ae6-44f7-ab56-d14dab8a4657").Build();
    let apiName = apiKey.["API name"]
    let apiKey = apiKey.["API key"]
    

    let bucket = "test"
    let deleteLocalFile file = File.Delete(file)

    [<Test>]
    member this.``WriteFile``() =
        let fileName = sprintf "test_%d.txt" (DateTime.Now.Ticks)
        let file = sprintf "C:/temp/test_%d.txt" (DateTime.Now.Ticks)
        try
            let content = "aaa"
            use writer = File.CreateText(file)
            writer.Write(content)
            writer.Flush()
            writer.Dispose()

            let client = Client(Satellites.EuropeWest_1, apiName, apiKey)            
            //let data = System.Text.Encoding.UTF8.GetBytes(content)

            let data = Array.zeroCreate<byte>(1024*1024)
            // execute
            client.WriteFile(bucket, fileName, data)

            ()

        finally
            deleteLocalFile file

