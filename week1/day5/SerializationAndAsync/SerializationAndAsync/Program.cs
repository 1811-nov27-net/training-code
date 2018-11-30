using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationAndAsync
{
    public class Program
    {
        // in VS format file with Ctrl+K, Ctrl+D
        // consistent formatting is good
        // in VS code it's Alt+Shift+F
        public static void Main(string[] args)
        {
            //var list = GetPeople();

            // backslashes are an "escape character" in strings like this
            // to treat them literally, use an @-string
            var fileName = @"C:\Users\Revature\Desktop\data.xml";

            Task<List<Person>> listTask = DeserializeFromFileAsync(fileName);

            // at this point in time, i have not yet started reading the file

            // synchronously wait on the task to get the return value.
            List<Person> list = listTask.Result;
            // only do .Result in Main ()

            list[0].Id *= 2; // make some change cause why not
            Console.WriteLine(list[0].Name.MiddleName);
            SerializeToFile(fileName, list);

            // gets IOException because opening same file all at same time
            //var results = DeserializeManyAsync(fileName).Result;
        }

        public static async Task<List<Person>[]> DeserializeManyAsync(string fileName)
        {
            var taskList = new List<Task<List<Person>>>();
            // get 10 tasks to deserialize the file
            for (int i = 0; i < 10; i++)
            {
                taskList.Add(DeserializeFromFileAsync(fileName));
            }

            // await all at the same time
            List<Person>[] allResults = await Task.WhenAll(taskList);
            return allResults;
        }

        public static void SerializeToFile(string fileName, List<Person> people)
        {
            // first, we need to convert the data in memory (the list of person)
            // into some byte representation (aka serial representation)
            // we can use many formats for this - we could make up our own,
            // or we could use JSON, or we could use XML, or some other format
            // we'll use XML

            // xmlserializer should really be generic but they've never updated it

            // "reflection" is when C# 'looks at itself', it let's you for example
            // iterate through all the properties of a class in code.
            var serializer = new XmlSerializer(typeof(List<Person>));

            // second, we need to write that representation to a file.
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fileStream, people);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Some error in file I/O: {e.Message}");
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        // as soon as we start doing something async:
        // 1. await some async method.
        // 2. make your method async, returning Task, and named ...Async.
        // 3. repeat with all methods that call that method, on and on

        // async on a method is just a flag to tell people that this returns
        // Task and it needs to be awaited to actually get the result.
        public static async Task<List<Person>> DeserializeFromFileAsync(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            List<Person> result;

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    // copy the fileStream asynchronously into the memoryStream.
                    await fileStream.CopyToAsync(memoryStream);
                    // when we await a task, other code can run in the meantime
                    // (like on another thread) - our web server can receive
                    // other requests, etc. - and WHEN the operation is done,
                    // this method will "unpause".
                } // will automatically call .Dispose as though it was in "finally"

                memoryStream.Position = 0; // reset "cursor" of memoryStream to beginning

                // doesn't support generics, returns "object", have to explicitly cast.
                result = (List<Person>)serializer.Deserialize(memoryStream);
            }
            return result;
        }

        public static List<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person
                {
                    Id = 20,
                    Name = new Name
                    {
                        FirstName = "Nicholas",
                        MiddleName = "A.",
                        LastName = "Escalona"
                    },
                    Address = new Address
                    {
                        Street = "123 Main St.",
                        City = "Arlington",
                        State = "TX"
                    },
                    Age = 26,
                    Nicknames = new List<string> { "Nick" }
                },
                new Person
                {
                    Name = new Name
                    {
                        FirstName = "Fred",
                        LastName = "Belotte"
                    }
                }
            };
        }
    }
}
