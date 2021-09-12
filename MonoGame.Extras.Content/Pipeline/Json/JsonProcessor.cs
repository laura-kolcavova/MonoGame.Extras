namespace MonoGame.Extars.Content.Pipeline.Json
{
    using Microsoft.Xna.Framework.Content.Pipeline;
    using System;
    using System.ComponentModel;

    [ContentProcessor(DisplayName = "JSON Processor")]
    public class JsonProcessor : ContentProcessor<ImporterResult<string>, JsonProcessorResult>
    {
        [DefaultValue(typeof(Type), "System.Object")]
        public string ContentType { get; set; }

        public override JsonProcessorResult Process(ImporterResult<string> input, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing JSON");

            var result = new JsonProcessorResult()
            {
                ContentType = ContentType,
                Json = input.Data
            };

            return result;
        }
    }
}
