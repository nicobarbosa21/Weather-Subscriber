using System.IO;

public class SubscriberService
{
    private readonly string _csvPath = "subscribers.csv";

    public IEnumerable<Subscriber> GetSubscribers()
    {
        if (!File.Exists(_csvPath))
            return Enumerable.Empty<Subscriber>();

        return File.ReadAllLines(_csvPath)
            .Skip(1)
            .Select(line => line.Split(','))
            .Where(parts => parts.Length == 3)
            .Select(parts => new Subscriber { Name = parts[0], Email = parts[1], City = parts[2] });
    }
    
    public void AddSubscriber(Subscriber subscriber)
    {
        var subscribers = GetSubscribers().ToList();
        if (!subscribers.Any(s => s.Email == subscriber.Email))
        {
            using var writer = File.AppendText(_csvPath);
            writer.WriteLine($"{subscriber.Name},{subscriber.Email},{subscriber.City}");
        }
    }

    public void RemoveSubscriber(string email)
    {
        var subscribers = GetSubscribers().Where(s => s.Email != email).ToList();
        File.WriteAllLines(_csvPath, new[] { "Name,Email,City" }.Concat(subscribers.Select(s => $"{s.Name},{s.Email},{s.City}")));
    }
}