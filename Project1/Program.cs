
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Project1
{
    public class TZ
    {

        public class MySerializable : System.Attribute { }


        public class Person
        {
            [MySerializable]
            public int age { get; set; }
            [MySerializable]
            public string name { get; set; }

            public Person  SomeProperty { get; internal set; }

          


        }

        public class Person1
        {
            [MySerializable]
            public int age { get; set; }
            [MySerializable]
            public string name { get; set; }
        }


        static void Main(string[] args)

        {


            Person person = new Person();
          
            person.age = 9;
            person.name = "Test";

            Person1 person2 = new Person1();
            person2.age = 11;
            person2.name = "Test2";

           

            var wrap = new Wrapper { SomeProperty = new Person { age = person.age, name =person.name } };
           
            Type type = wrap.GetType();

            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.FieldType == typeof(Person))
                {
                    var properties = from t in Assembly.GetExecutingAssembly().GetTypes()
                    from p in t.GetProperties()
                    let attrs = p.GetCustomAttributes(typeof(MySerializable), true)
                    where attrs != null && attrs.Length > 0
                    select p;

                    Console.WriteLine(properties.Count());

                    foreach (var pr in properties)
                    {
                        Console.WriteLine(pr.Name);
                    }
                    string json = JsonConvert.SerializeObject(person, Formatting.Indented);
                    string json1 = JsonConvert.SerializeObject(person2, Formatting.Indented);

                    File.WriteAllText(@"C:\Users\user\Desktop\Test1.txt", json);
                    File.WriteAllText(@"C:\Users\user\Desktop\Test.txt", json1);
                    Console.WriteLine(json);
                    Console.WriteLine(json1);



                }
                else 
                {
                    break;
                }
            }
           
            Person pr1 = new Person
            {
                SomeProperty = new Person
                {

                   age =person.age,
                   name =person.name


                }
            };
            try
            {
                Type type1 = pr1.GetType();
                PropertyInfo info = type.GetProperty("SomeProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                Person value = (Person)info.GetValue(pr1, null);

            }
            catch
            {
                
            }
            
            // use `value` variable here
        }


        //var types = from t in Assembly.GetExecutingAssembly().GetTypes()
        //            from p in t.GetProperties()
        //            let attrs = p.GetCustomAttributes(typeof(MySerializable), true)
        //            where attrs != null && attrs.Length > 0
        //            select t;

        //foreach (var tp in types)
        //{
        //    var prs = from k in tp.GetProperties()
        //              let attrs = k.GetCustomAttributes(typeof(MySerializable), true)
        //              where attrs != null && attrs.Length > 0
        //              select k;
        //    foreach (var pr in prs)
        //    {
        //        PropertyInfo propertyInfo = pr;
        //        Console.WriteLine(propertyInfo.Name);
        //        ;
        //        Console.WriteLine(propertyInfo.GetValue(tp.MakeGenericType(tp)));
        //    }
        //}

        //Console.WriteLine(properties.Count());
        //foreach (var pr in properties)
        //{
        //    PropertyInfo propertyInfo = pr;
        //    Console.WriteLine(pr.Name);
        //    Console.WriteLine(pr.ReflectedType.Name);
        //    Console.WriteLine(pr.Name + " - " + pr.GetValue(propertyInfo.ReflectedType).ToString());
        //}


    }
    public static class Serializer
    {
      
        public static string ToJson(object target)
        {

            return "";
        }
        public static T FromJson<T>(string json)
        {

            return default(T);
        }
    }
}
