namespace G19_20231125
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\Gela.txt";

            Person[] array = new Person[]
            {
                new Person {Id= 20, Firstname ="Zaza", Lastname="Beradze", BirthDate= new DateTime(1994, 3,4),Gender= GenderType.Male },
                new Person {Id= 30,Firstname= "Shieda", Lastname ="kayn", BirthDate= new DateTime(2004, 4,4), Gender= GenderType.Male }
            };

            Person person1 = new Person
            {
                Id = 1,
                Firstname = "Nika",
                Lastname = "Chichua",
                BirthDate = new DateTime(2004, 8, 4),
                Gender = GenderType.Male
            };
            Person person2 = new Person(2, "Gulbaza", "Gulbazadze", new DateTime(1993, 3, 1), GenderType.Male);

            Person person3 = new Person
            {
                Id = 3,
                Firstname = "Gela",
                Lastname = "Geladze",
                BirthDate = new DateTime(2022, 1, 3),
                Gender = GenderType.Male
            };
            Person person4 = new Person(4, "NikaJr", "Chichua", new DateTime(2030, 3, 1), GenderType.Male);

            //person1.Children.Add(person4);

            PersonList2 personList = new();
            personList.Add(person1);
            personList.Add(person2);
            personList.Add(person3);

            PersonList personList2 = new();
            personList2.Add(person4);


            //for (int i = 0; i < array.Length; i++)
            //{
            //    Console.WriteLine($"{array[i]}");
            //}
            //personList[0]= person2;
            //personList.Add(person4);          
            //Console.WriteLine(personList.Contains(person4));
            //personList.Insert(2, person3);

            //personList.CopyTo(array, 0);
            personList.InsertRange(3, array);


            //for (int i = 0; i < personList.Count; i++)
            //{
            //    Console.WriteLine($"{personList[i]}");
            //}


            //personList.Clear();
            //personList.Remove(person1);
            //personList.RemoveAt(0);
            //personList.InsertRange

            //personList.Load(filePath);

            personList.Save(filePath);
            //personList.Load(filePath);
            Console.WriteLine();
           
            foreach (var p in personList)
            {
                Console.WriteLine(p);
            }
        }
    }
}