using Newtonsoft.Json;
namespace ConsolePhoneBook;

internal class Contact
{
    public Contact(int id, string name, string number)
    {
        ID = id;
        Name = name;
        Number = number;
    }

    [JsonProperty(nameof(ID))]
    internal int ID { get; set; }

    [JsonProperty(nameof(Name))]
    internal string Name { get; set; }

    [JsonProperty(nameof(Number))]
    internal string Number { get; set; }
}
