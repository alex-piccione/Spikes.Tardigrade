namespace Alex75.Tardigrade

open uplink.NET
open uplink.NET.Services
open System.Threading

type Satellites() =
    static member EuropeWest_1 = "europe-west-1.tardigrade.io:7777"

type Client(satelliteUrl:string, apiName:string, apiKey:string) =


    member this.ReadFile(bucket:string, fileName:string):byte[] =

        use access = new Models.Access(satelliteUrl, apiName, apiKey)
        //let bucket_ = BucketService(access).GetBucketAsync(bucket).Result
        let bucket_ = BucketService(access).EnsureBucketAsync(bucket).Result
        let service = ObjectService(access)
        let object_ = service.GetObjectAsync(bucket_, fileName).Result
        //object_.SystemMetaData.ContentLength
        use download = service.DownloadObjectAsync(bucket_, fileName, Models.DownloadOptions(), true).Result
        download.DownloadedBytes

    member this.WriteFile(bucket:string, fileName:string, content:byte[]) =
        // api name and secret are inverted
        use access = new Models.Access(satelliteUrl, apiKey, apiName)
        let bucket_ = BucketService(access).EnsureBucketAsync(bucket).Result
        //let bucket_ = getBucket access bucket_ BucketService(access).GetBucketAsync(bucket).Result
        let service = ObjectService(access)

        let operation = service.UploadObjectAsync(bucket_, fileName, Models.UploadOptions(), content, true).Result
        while not(operation.Completed) do 
            Thread.Sleep 100

        //let tot= operation.BytesSent
        //let f = operation.Failed
        ()

