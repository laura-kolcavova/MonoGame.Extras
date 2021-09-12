namespace MonoGame.Extars.Content.Pipeline.Json
{
    using Microsoft.Xna.Framework.Content.Pipeline;
    using System.IO;

    [ContentImporter(".json", DefaultProcessor = nameof(JsonProcessor),
    DisplayName = "JSON Importer - MonoECS")]
    public class JsonImporter : ContentImporter<ImporterResult<string>>
    {
        public override ImporterResult<string> Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing JSON file: {0}", filename);

            using (StreamReader reader = File.OpenText(filename))
            {
                string json = reader.ReadToEnd();
                return new ImporterResult<string>(filename, json);
            }
        }
    }
}
