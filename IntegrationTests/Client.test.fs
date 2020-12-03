module IntegrationTests

open System.IO
open NUnit.Framework



type ClientTest () =

    [<Test>]
    member this.``WriteFile``() =

        let content = "aaa"
        use writer = File.CreateText("C:/temp/file")
        writer.Write(content)
        writer.Flush()
        writer.Dispose()


