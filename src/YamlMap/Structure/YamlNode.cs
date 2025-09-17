using System.Collections.Generic;
using System.Linq;

namespace YamlMap
{
    /// <summary>
    /// 
    /// </summary>
    public class YamlNode : IYamlNode
    {
        private readonly Dictionary<string, IYamlNode> _children = [];

        /// <summary>
        /// Gets the YamlNode associated with the key
        /// </summary>
        /// <param name="key"></param>
        public IYamlNode this[string key]
        {
            get
            {
                if (!_children.ContainsKey(key))
                {
                    return null;
                }
                
                return _children[key];
            }
        }

        /// <summary>
        /// Gets the YamlNode at the indicated index
        /// </summary>
        /// <param name="index"></param>
        public IYamlNode this[int index] {
            get
            {
                if (_children.Count > index && index >= 0)
                {
                    var key = _children.Keys.ElementAt(index);
                    return _children[key];
                }
                
                return null;
            }
        }
        
        /// <summary>
        /// Returns the children of the node
        /// </summary>
        public object Value => _children;

        /// <summary>
        /// Set the child value of the node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, IYamlNode value)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = _children.Count.ToString();
            }
            
            _children[key] = value;
        }
    }
}