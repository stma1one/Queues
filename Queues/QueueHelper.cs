using Queues.Models;
using System;

//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    public class QueueHelper
    {
        /// <summary>
        /// פעולת ספירת כמות איברים בתור 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public static int Count<T>(Queue<T> q)
        {
            int counter = 0;
            //ניצור עותק נוסף של התור
            Queue<T> temp = Copy(q);
            //נרוקן את העותק
            while (!temp.IsEmpty())
            {
                counter++;
                temp.Remove();
            }
            //נחזיר את הכמות
            return counter;
        }
        /// <summary>
        /// פעולה הסופרת כמות איברים בתור ללא שימוש בפעולת עזר
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>

        public static int Count2<T>(Queue<T> q)
        {
            int counter = 0;
            Queue<T> temp = new Queue<T>();
            //נעתיק את הערכים לתור חדש
            while (!q.IsEmpty())
            {
                temp.Insert(q.Remove());
                counter++;
            }
            //נחזיר את הערכים חזרה לתור המקורי
            while (!temp.IsEmpty())
            {
                q.Insert(temp.Remove());
            }
            //נחזיר את הכמות
            return counter;
        }
        /// <summary>
        /// פעולה היוצרת עותק של התור
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Queue<T> Copy<T>(Queue<T> original)
        {
            Queue<T> copy = new Queue<T>();
            Queue<T> temp = new Queue<T>();
            while (!original.IsEmpty())
            {
                temp.Insert(original.Remove());

            }
            while (!temp.IsEmpty())
            {
                copy.Insert(temp.Head());
                original.Insert(temp.Remove());
            }
            return copy;

        }

        public static bool IsOle(Queue<int> q)
        {
            if (q.IsEmpty())
                return true;
            Queue<int> temp = Copy(q);
            int last = temp.Remove();
            while (!temp.IsEmpty())
            {
                if (last > temp.Head())
                    return false;
                last = temp.Remove();
            }
            return true;

        }

        public static int FindMin(Queue<int> q)
        {
            if (q.IsEmpty())
                return int.MinValue;
            Queue<int> temp = Copy(q);
            int min = temp.Remove();
            while (!temp.IsEmpty())
            {
                if (min > temp.Head())
                    min = temp.Head();
                temp.Remove();
            }
            return min;


        }

        public static void RemoveMinFromQueue(Queue<int> q)
        {
            int min = FindMin(q);
            Queue<int> temp = new Queue<int>();
            while (!q.IsEmpty())
            {
                if (q.Head() != min)
                    temp.Insert(q.Remove());
                else
                    q.Remove();
            }

            while (!temp.IsEmpty())
            {
                q.Insert(temp.Remove());
            }
        }

        public static Queue<int> OrderAsc(Queue<int> q)
        {
            Queue<int> temp = Copy(q);//O(n)
            Queue<int> result = new Queue<int>();//O(1)
            while(!temp.IsEmpty()) // n
            {
                result.Insert(FindMin(temp));//O(n)
                RemoveMinFromQueue(temp);//O(n)
            }
            return result;
        }
        //O(n)+O{1)+n*(O(n)+O(n))===>O(n^2)

        public static void InsertToMiddle<T>(Queue<T> q, T value)
        {
            Queue<T> temp = new Queue<T>();//תור עזר    
            int count = Count(q);//מספר האיברים בתור
            for (int i = 0; i < count / 2; i++)//נכניס חצי מאיברי התור לתור העזר
                temp.Insert(q.Remove());
            temp.Insert(value);//נכניס את האיבר החדש לתור העזר (בדיוק באמצע)
            while (!q.IsEmpty())
                temp.Insert(q.Remove());//נעביר את החצי השני לתור העזר
            while (!temp.IsEmpty())
                q.Insert(temp.Remove());//נחזיר את כל האיברים לתור המקורי
        }
        public static int CountWithDummy(Queue<int> q) 
        {
            //רק אם ידוע לנו משהו על החוקיות של הערכים בתור
            //נוכל להשתמש באפשרות זו

            
            int counter = 0;
            q.Insert(-1);//ידוע שכל הערכים בתור גדולים מ-0
            while(q.Head()!=-1)//נדע שהגענו לסוף התור אם הגענו לערך הלא חוקי שהכנסנו
            {
                counter++;
                q.Insert(q.Remove());//נדחוף את האיבר שהוצאנו בצורה מעגלית (כלומר לסוף התור)
            }
            q.Remove();//לא לשכוח להעיף את הערך הלא חוקי שנמצא בראש התור
            return counter;

        }
        public static int CountRecursive(Queue<int> q,Queue<int> temp)
        {
            if (q.IsEmpty())//אם התור ריק נחזיר 0 - תנאי עצירה
                return 0;
            temp.Insert(q.Remove());//נשמור בתור העזר את הערך שהוצאנו
            int result = 1 + CountRecursive(q,temp);//נספור 1 + כמות האיברים בשאר התור
            q.Insert(temp.Remove());//נחזיר את התור למצבו המקורי בחזור
            return result;//נחזיר את המונה שלנו

            //עבור התור 
            //   1->5->6->7    7 ראש התור
        }
    }
}
