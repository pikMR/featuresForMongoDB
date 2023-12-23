> This proyect console can add news features for use in your mongoDb collections.
> 
> Now you can use two features for example: CreateRandomGuids and UpdatePropertiesWithCsv.
>
> You can add easily your feature into FeaturesForMongoDB\Impl and use ContextFromMongoDb for create/update mongodb or filejson


## You need use Features like this:

- With mongodb collection, use one document and repeat (appsettings.NumToDuplicate) with random guid (CSSUID/base64/.NET).
  - You need set appsettings with ImplementationsToCreate -> CreateRandomGuids
  - You can create a file, you need set appsettings with CreateJson (true / false)
  - You can update a mongodbCollection, you need set appsettings with UpdateCollection (true / false)
    #
    

- With mongodb collection, use one collection and update or create with a csv file(appsettings.CsvFile and appsettings.FilePath).
  - You need set appsettings with ImplementationsToUpdate -> UpdatePropertiesWithCsv
  - You can create a file, you need set appsettings with CreateJson (true / false)
  - You can update a mongodbCollection, you need set appsettings with UpdateCollection (true / false)
    #


## License

MIT

**Free Software, Hell Yeah!**