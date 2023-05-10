using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritm
{
    public class BinaryTreeNode
    {
        public string Operator { get; set; }
        public double Value { get; set; }
        public BinaryTreeNode Left { get; set; } = null;
        public BinaryTreeNode Right { get; set; } = null;
    }
}
