namespace MonoGame.Extars.Content.Pipeline
{
    public class ImporterResult<T>
    {
        public ImporterResult(string filePath, T data)
        {
            FilePath = filePath;
            Data = data;
        }

        public string FilePath { get; }
        public T Data { get; }
    }
}
