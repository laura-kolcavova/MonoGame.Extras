namespace MonoGame.Extars.Content.Pipeline.Json
{
    using Microsoft.Xna.Framework.Content.Pipeline;
    using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

    [ContentTypeWriter]
    internal class JsonContentTypeWriter : ContentTypeWriter<JsonProcessorResult>
    {
        private string _runtimeType;

        protected override void Write(ContentWriter output, JsonProcessorResult value)
        {
            _runtimeType = value.ContentType;
            output.Write(value.Json);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return _runtimeType;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return _runtimeType;
        }
    }
}
