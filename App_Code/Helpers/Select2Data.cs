using System.Collections.Generic;

public class Select2Data
{
    public Paginate Paginate { get; set; }
    public List<Child> Results { get; set; }
}

public class Result
{
    public List<Child> Children { get; set; }
    public string Text { get; set; }
}

public class Child
{
    public long id { get; set; }
    public string text { get; set; }

}

public class Paginate
{
    public bool More { get; set; }
}