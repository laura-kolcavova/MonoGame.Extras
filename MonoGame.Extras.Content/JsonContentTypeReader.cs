namespace MonoGame.Extars.Content
{
    using Microsoft.Xna.Framework.Content;
    using Newtonsoft.Json;

    public class JsonContentTypeReader<T> : ContentTypeReader<T>
    {
        protected override T Read(ContentReader reader, T existingInstance)
        {
            var json = reader.ReadString();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
