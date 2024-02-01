using System;
using System.Diagnostics.Metrics;
using DataStructureCore;
namespace Queues
{
    internal class Program
    {

        public static int Count<T>(Queue<T> q)
        {
            int counter = 0;
            //ניצור עותק נוסף של התור
            Queue<T> temp = Copy(q);
            //נרוקן את העותק
            while(!temp.IsEmpty())
            {
                counter++;
                temp.Remove();
            }
            //נחזיר את הכמות
            return counter;
        }

        public static int Count2<T>(Queue<T> q)
        {
            int counter = 0;
            Queue<T> temp = new Queue<T>();
            //נעתיק את הערכים לתור חדש
            while(!q.IsEmpty()) 
            {
                temp.Insert(q.Remove());
                counter++;
            }
            //נחזיר את הערכים חזרה לתור המקורי
            while(!temp.IsEmpty())
            {
                q.Insert(temp.Remove());
            }
            //נחזיר את הכמות
            return counter;
        }
        public static Queue<T> Copy<T> (Queue<T> original) 
        {
            Queue<T> copy=new Queue<T> ();
            Queue<T> temp = new Queue<T>();
            while(!original.IsEmpty())
            {
                temp.Insert(original.Remove());

            }
            while(!temp.IsEmpty())
            {
                copy.Insert(temp.Head());
                original.Insert(temp.Remove());
            }
            return copy;

        }
        static void Main(string[] args)
        {
            Queue<int> q1= new Queue<int>();    
            q1.Insert(1);
            q1.Insert(2);
            q1.Insert(3);
            q1.Insert(4);
            q1.Insert(5);
            Console.WriteLine(q1);
            Console.WriteLine(Count(q1));
        }
    }
}