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
            List<Vector2> Allvalue = new List<Vector2>();
            Root.AllValues(Allvalue);
            var temp1 = Allvalue.OrderBy(x => x.X);
            var temp2 = collection.OrderBy(x => x.X);
        }

        public List<Vector2> GetListOfItemsInRange(Vector2 point, float range)
        {
            var temp =  GetListOfItemsWhere(point.X + range, point.X - range, point.Y + range, point.Y - range, RootOrderedByX, Root);
            return temp;
        }

        private List<Vector2> GetListOfItemsWhere(float maxX, float minX, float maxY, float minY, bool XOrdered, kdTreeNode currentNode)
        {
            List<Vector2> list = new List<Vector2>();
            if(currentNode == null)
            {
                return list;
            }
            if(currentNode.Value.X >= minX && currentNode.Value.X <= maxX && currentNode.Value.Y >= minY && currentNode.Value.Y <= maxY)
            {
                list.Add(currentNode.Value);
            }
            if (XOrdered) //Level is ordered by X
            {
                if (currentNode.Value.X >= minX) //if the value of the currentnode is more than the minimum, call the function on the left child.
                {
                    var values = GetListOfItemsWhere(maxX, minX, maxY, minY, !RootOrderedByX, currentNode.LeftChild);
                    foreach(var i in values)
                    {
                        list.Add(i);
                    }
                }
                if (currentNode.Value.X <= maxX) //if the value of the currentnode is more than the minimum, call the function on the right child.
                {
                    var values = GetListOfItemsWhere(maxX, minX, maxY, minY, !RootOrderedByX, currentNode.RightChild);
                    foreach (var i in values)
                    {
                        list.Add(i);
                    }
                }
            }
            else//if this level is ordered by Y
            {
                if (currentNode.Value.Y >= minY)
                {
                    var values = GetListOfItemsWhere(maxX, minX, maxY, minY, !RootOrderedByX, currentNode.LeftChild);
                    foreach (var i in values)
                    {
                        list.Add(i);
                    }
                }
                if (currentNode.Value.Y <= maxY)
                {
                    var values = GetListOfItemsWhere(maxX, minX, maxY, minY, !RootOrderedByX, currentNode.RightChild);
                    foreach (var i in values)
                    {
                        list.Add(i);
                    }
                }
            }
            return list;
        }

        //private void BuildTree(bool LayerOrderedByX, )

        private class kdTreeNode
        {
            public Vector2 Value;
            public kdTreeNode LeftChild, RightChild;

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
                    collection = collection.OrderBy(x => x.X).ToArray();
                }
                else
                {
                    collection = collection.OrderBy(x => x.Y).ToArray();
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

            public void AllValues(List<Vector2> list)
            {
                list.Add(Value);
                if (LeftChild != null)
                {
                    LeftChild.AllValues(list);
                }
                if (RightChild != null)
                {
                    RightChild.AllValues(list);
                }
            }

            public int CountElements(bool test)
            {
                int total = 1;
                if (LeftChild != null)
                {
                    if (test)
                    {
                        if(Value.X < LeftChild.Value.X)
                        {
                            int a = 0;
                        }
                    }
                    else
                    {
                        if (Value.Y < LeftChild.Value.Y)
                        {
                            int a = 0;
                        }
                    }
                    total += LeftChild.CountElements(!test);
                }
                if (RightChild != null)
                {
                    if (test)
                    {
                        if (Value.X > RightChild.Value.X)
                        {
                            int a = 0;
                        }
                    }
                    else
                    {
                        if (Value.Y > RightChild.Value.Y)
                        {
                            int a = 0;
                        }
                    }
                    total += RightChild.CountElements(!test);
                }
                return total;
            }
        }
    }
}
