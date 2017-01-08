using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class kdTree
    {
        bool Xordered;
        node Root;
        public kdTree(IEnumerable<Vector2> collection)
        {
            Xordered = true;
            var arraycollection = collection.ToArray();
            arraycollection.OrderBy(x => x.X);

            Root = new node();
            Root.ConstructTree(arraycollection, Xordered);
            Root.TestValues();
        }
        

        public List<Vector2> ReturnWhithinRange(Vector2 point, float radius)
        {
            List<Vector2> toreturn = new List<Vector2>();
            var templist = new List<Vector2>();
            Root.AddToListWhereInSquare(templist, point.X - radius, point.X + radius, point.Y - radius, point.Y + radius);
            foreach(var i in templist)
            {
                if(Vector2.Distance(i, point) <= radius)
                {
                    toreturn.Add(i);
                }
            }
            return toreturn;
        }

        class node
        {
            Vector2 value;
            node Left, Right;
            bool Xordered;

            public void TestValues()
            {
                if (Left != null)
                {
                    if (Xordered)
                    {
                        if (value.X < Left.value.X)
                        {
                            breakp();
                        }
                    }
                    else
                    {
                        if (value.Y < Left.value.Y)
                        {
                            breakp();
                        }
                    }
                    Left.TestValues();
                }
                if (Right != null)
                {
                    if (Xordered)
                    {
                        if (value.X > Right.value.X)
                        {
                            breakp();
                        }
                    }
                    else
                    {
                        if (value.Y > Right.value.Y)
                        {
                            breakp();
                        }
                    }
                    Right.TestValues();
                }
            }

            private void breakp()
            {
                int lol = 3;
            }
            
            public void AddToListWhereInSquare(List<Vector2> list, float xmin, float xmax, float ymin, float ymax)
            {
                if(value.X >= xmin && value.X <= xmax && value.Y >= ymin && value.Y <= ymax)
                {
                    list.Add(value);
                    if(Left != null)
                    Left.AddToListWhereInSquare(list, xmin, xmax, ymin, ymax);
                    if (Right != null)
                        Right.AddToListWhereInSquare(list, xmin, xmax, ymin, ymax);
                }
                else
                {
                    if (Xordered)
                    {
                        if (value.X < xmax)
                        {
                            if(Right != null)
                                Right.AddToListWhereInSquare(list, xmin, xmax, ymin, ymax);
                        }
                        if( value.X > xmin)
                        {
                            if (Left != null)
                                Left.AddToListWhereInSquare(list, xmin, xmax, ymin, ymax);
                        }
                    }
                    else
                    {
                        if (value.Y < ymax)
                        {
                            if (Right != null)
                                Right.AddToListWhereInSquare(list, xmin, xmax, ymin, ymax);
                        }
                        if (value.Y > ymin)
                        {
                            if (Left != null)
                                Left.AddToListWhereInSquare(list, xmin, xmax, ymin, ymax);
                        }
                    }
                }
            }

            public void ConstructTree(Vector2[] array, bool Xordered)
            {
                this.Xordered = Xordered;
                if (Xordered)
                {
                    array = array.OrderBy(i => i.X).ToArray();
                }
                else
                {
                    array = array.OrderBy(i => i.Y).ToArray();
                }
                this.value = array[ array.Length / 2];
                if(array.Length >= 3)
                {
                    Vector2[] leftarray, rightarray;
                    leftarray = new Vector2[array.Length / 2];
                    rightarray = new Vector2[array.Length - (array.Length / 2 + 1)];
                    Array.Copy(array, 0, leftarray, 0, leftarray.Length);
                    Array.Copy(array, array.Length / 2 + 1, rightarray, 0, rightarray.Length);
                    Left = new node();
                    Right = new node();
                    Left.ConstructTree(leftarray, !Xordered);
                    Right.ConstructTree(rightarray, !Xordered);
                }
                else
                {
                    if(array.Length == 1)
                    {
                        value = array[0];
                        return;
                    }
                    if(array.Length == 2)
                    {
                        value = array[1];
                        Left = new node();
                        Left.ConstructTree(new Vector2[] { array[0] }, !Xordered);
                    }
                }
            }
        }
    }
}
