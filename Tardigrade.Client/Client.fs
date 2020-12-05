namespace Alex75.Tardigrade

open uplink.NET
open uplink.NET.Services
open System.Threading

type Satellites() =
    static member EuropeWest_1 = "europe-west-1.tardigrade.io:7777"

type Client(satelliteUrl:string, keyName:string, keySecret:string) =


    member this.ReadFile(bucket:string, fileName:string):byte[] =
        use access = new Models.Access(satelliteUrl, keySecret, keyName)
        let bucket_ = BucketService(access).EnsureBucketAsync(bucket).Result
        let service = ObjectService(access)
        use download = service.DownloadObjectAsync(bucket_, fileName, Models.DownloadOptions(), true).Result
        while not(download.Completed) do 
            Thread.Sleep 100
        download.DownloadedBytes


    member this.WriteFile(bucket:string, fileName:string, content:byte[]) =
        
        use access = new Models.Access(satelliteUrl, keySecret, keyName)  // // api name and secret are inverted
        let bucket_ = BucketService(access).EnsureBucketAsync(bucket).Result
        let service = ObjectService(access)
                
        let upload = service.UploadObjectAsync(bucket_, fileName, Models.UploadOptions(), content, true).Result
        while not(upload.Completed) do 
            Thread.Sleep 100


    member this.DeleteFile (bucket:string, fileName:string) =

        use access = new Models.Access(satelliteUrl, keySecret, keyName)
        let bucket_ = BucketService(access).EnsureBucketAsync(bucket).Result
        let service = ObjectService(access)
        
        service.DeleteObjectAsync(bucket_, fileName) |> Tasks.Task.WaitAll
