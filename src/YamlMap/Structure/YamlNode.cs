using System.Collections.Generic;
using System.Linq;

namespace YamlMap
{
    /// <summary>
    /// 
    /// </summary>
    public class YamlNode : IYamlNode
    {
        private readonly Dictionary<string, IYamlNode> _nodes = [];

        public YamlNode()
        {
        }

        public YamlNode(string key)
        {
            Key = key;
        }
        
        /// <summary>
        /// Gets the YamlNode associated with the key
        /// </summary>
        /// <param name="key"></param>
        public IYamlNode this[string key] => !_nodes.TryGetValue(key, out var item) ? null : item;

        /// <summary>
        /// Gets the YamlNode at the indicated index
        /// </summary>
        /// <param name="index"></param>
        public IYamlNode this[int index] {
            get
            {
                if (_nodes.Count > index && index >= 0)
                {
                    var key = _nodes.Keys.ElementAt(index);
                    return _nodes[key];
                }
                
                return null;
            }
        }
        
        /// <summary>
        /// Gets the name of th Property of the node
        /// </summary>
        public string Key { get; }
        
        /// <summary>
        /// Returns the children of the node
        /// </summary>
        public object Value => _nodes;
        
        /// <summary>
        /// Gets the child nodes
        /// </summary>
        public IEnumerable<IYamlNode> Nodes => _nodes.Values;
        
        /// <summary>
        /// Set the child value of the node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, IYamlNode value)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = _nodes.Count.ToString();
            }
            
            _nodes[key] = value;
        }
    }
}