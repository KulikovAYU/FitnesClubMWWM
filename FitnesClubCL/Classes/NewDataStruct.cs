using System.Collections.Generic;

namespace FitnesClubCL.Classes
{
 /// <summary>
 /// Новая структура для облегчения работы
 /// </summary>
 /// <typeparam name="T1"></typeparam>
 /// <typeparam name="T2"></typeparam>
   public class CustomDictionary<T1,T2> : Dictionary<T1, T2>
 {
       public bool IsEmpty => Count == 0;

     // Dictionary<string, object>
   }
}
