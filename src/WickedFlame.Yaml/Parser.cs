
namespace WickedFlame.Yaml
{
    public class Parser
    {
        private readonly IScanner _scanner;

        public Parser(IScanner scanner)
        {
            _scanner = scanner;
        }

        public IToken Parse()
        {
            var line = _scanner.ScanNext();
            var root = new Token(null, 0);
            IToken token = root;

            while (line != null)
            {
                while (token.Parent != null && line.Indentation <= token.Indentation)
                {
                    if (token.Parent == null)
                    {
                        break;
                    }

                    token = token.Parent;
                }
                
                if (line.IsListItem && !string.IsNullOrEmpty(line.Property) && !string.IsNullOrEmpty(line.Value))
                {
                    // add new object to the tree
                    var list = new Token(null, line.Indentation, TokenType.ListItem);

                    // list item
                    token.Set(list);
                    token = list;

                    var value = new ValueToken(line.Property, line.Value, line.Indentation + 2);
                    token.Set(value);

                    line = _scanner.ScanNext();
                    continue;
                }

                if (line.IsListItem && !string.IsNullOrEmpty(line.Property))
                {
                    // add new list to the tree
                    var list = new Token(null, line.Indentation, TokenType.ListItem);
                    token.Set(list);
                    token = list;


                    // object node to list
                    var child = new Token(line.Property, line.Indentation + 2);
                    token.Set(child);
                    token = child;

                    line = _scanner.ScanNext();
                    continue;
                }


                // simple list item eg: List<string>
                if (line.IsListItem)
                {
                    var child = new ValueToken(line.Property, line.Value, line.Indentation + 2);
                    token.Set(child);

                    line = _scanner.ScanNext();
                    continue;
                }


                // simple property with value
                if (!string.IsNullOrEmpty(line.Property) && !string.IsNullOrEmpty(line.Value))
                {
                    var child = new ValueToken(line.Property, line.Value, line.Indentation);
                    token.Set(child);

                    line = _scanner.ScanNext();
                    continue;
                }

                if (!string.IsNullOrEmpty(line.Property))
                {
                    // add new object to the tree
                    var child = new Token(line.Property, line.Indentation);

                    token.Set(child);
                    token = child;

                    line = _scanner.ScanNext();
                    continue;
                }

                line = _scanner.ScanNext();
            }

            return root;
        }
    }
}
