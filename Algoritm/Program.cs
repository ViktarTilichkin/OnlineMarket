using Algoritm;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        static public void IsertInTree(BinaryTreeNode root, BinaryTreeNode elem)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));
            if (elem == null) throw new ArgumentNullException(nameof(elem));
            if()
        }

        Exception 
        static public bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

    }