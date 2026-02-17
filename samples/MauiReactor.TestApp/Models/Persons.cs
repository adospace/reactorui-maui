using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Models;

public record Person(string FirstName, string LastName);

public class GroupOfPerson : List<Person>
{
    public GroupOfPerson(string initial)
    {
        Initial = initial;
    }

    public string Initial { get; }

    static GroupOfPerson[]? _all;
    public static GroupOfPerson[] All
    {
        get => _all ??= Setup();
    }

    static GroupOfPerson[] Setup()
    {
        var random = new Random();
        var firstNames = new[] { "Alice", "Bob", "Charlie", "Dave", "Emily", "Frank", "Grace", "Henry", "Isabelle", "Jack", "Kate", "Liam", "Mia", "Nora", "Oliver", "Penelope", "Quinn", "Riley", "Sofia", "Thomas", "Uma", "Violet", "Wyatt", "Xander", "Yara", "Zane" };
        var lastNames = new[] { "Adams", "Brown", "Clark", "Davis", "Evans", "Foster", "Garcia", "Harris", "Ingram", "Johnson", "Klein", "Lee", "Miller", "Nguyen", "O'Brien", "Perez", "Quinn", "Ramirez", "Smith", "Taylor", "Upton", "Valdez", "Wang", "Xu", "Yang", "Zhang" };

        var people = new List<Person>();
        for (int i = 0; i < 100; i++)
        {
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];
            people.Add(new Person(firstName, lastName));
        }

        var groups = new Dictionary<char, GroupOfPerson>();
        foreach (var person in people)
        {
            var initial = person.FirstName[0];
            if (!groups.ContainsKey(initial))
            {
                groups[initial] = new GroupOfPerson(initial.ToString());
            }
            groups[initial].Add(person);
        }

        foreach (var group in groups)
        {
            group.Value.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
        }

        return groups.OrderBy(_ => _.Key).Select(_ => _.Value).ToArray();
    }

}