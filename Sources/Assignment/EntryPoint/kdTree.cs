using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public class kdTree
    {
        kdTreeNode Root;
        bool RootOrderedByX;

        public kdTree(IEnumerable<Vector2> collection)
        {
            RootOrderedByX = true;
            Root = kdTreeNode.BuildTree(RootOrderedByX, collection.ToArray());
        }

        public List<Vector2> GetListOfItemsWhere(Func<Vector2, bool> ConditionalFunction)
        {
            throw new NotImplementedException();
        }

        //private void BuildTree(bool LayerOrderedByX, )

        private class kdTreeNode
        {
            Vector2 Value;
            kdTreeNode LeftChild, RightChild;

            /// <summary>
            /// Creates a new kd ordered tree, alternating the x and y ordering
            /// </summary>
            /// <param name="LayerOrderedByX"></param>
            /// <param name="collection"></param>
            /// <returns>A fully formed node with children.</returns>
            public static kdTreeNode BuildTree(bool LayerOrderedByX, Vector2[] collection)
            {
                if(collection.Length < 1) //If the collection has no elements, returns null
                {
                    return null;
                }
                var node = new kdTreeNode(); //create new node
                if(collection.Length == 1) // return only value in case the collection has only one value left.
                {
                    node.Value = collection[0];
                    return node;
                }
                int middle = collection.Length / 2;

                if (LayerOrderedByX) {
                    collection.OrderBy(x => x.X);
                }
                else
                {
                    collection.OrderBy(x => x.Y);
                }

                node.Value = collection[middle];
                Vector2[] left = new Vector2[middle];
                Vector2[] right = new Vector2[collection.Length - middle - 1];
                Array.Copy(collection, 0, left, 0, middle);
                Array.Copy(collection, middle + 1, right, 0, collection.Length - middle - 1);

                node.LeftChild = BuildTree(!LayerOrderedByX, left);
                node.RightChild = BuildTree(!LayerOrderedByX, right);
                return node;
            }

            
        }
    }
}
