
namespace YamlMap.Scanning
{
    public class QuotationReaderBase
    {
        private readonly char _mark;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readerType"></param>
        /// <param name="mark"></param>
        protected QuotationReaderBase(TokenReaderType readerType, char mark)
        {
            ReaderType = readerType;
            _mark = mark;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public TokenReaderType ReaderType { get; }
        
        /// <summary>
        /// Get the index of the next line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int IndexOfNext(string line)
        {
            var index = line.IndexOf(_mark);
            var property = line.IndexOf(':');
            var list = line.IndexOf('-');

            return index == 0 ||
                   property > 0 && index == property + 2 || 
                   list > 0 && index == list + 2 
                ? index 
                : -1;
        }
        
        /// <summary>
        /// Parse the next property
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Read(IScanner scanner, string input)
        {
            var start = input.IndexOf(_mark);
            var end = input.LastIndexOf(_mark);
            if (end == start || end < input.Length - 1)
            {
                var index = scanner.AddToIndex(0);
                throw new InvalidConfigurationException($"Found unexpected end of stream while scanning a quoted scalar at line {index + 1} column {end}");
            }

            return input;
        }
    }
}