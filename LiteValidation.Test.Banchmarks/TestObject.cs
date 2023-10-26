using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class TestObject
{

    public string Text1 { get; set; } = "a";
    public string Text2 { get; set; } = "b";
    public string Text3 { get; set; } = "c";
    public string Text4 { get; set; } = "d";
    public string Text5 { get; set; } = "e";

    public int Number1 { get; set; } = 1;

    public int Number2 { get; set; } = 2;

    public int Number3 { get; set; } = 3;

    public int Number4 { get; set; } = 4;

    public int Number5 { get; set; } = 5;

    public decimal? SuperNumber1 { get; set; } = 1;

    public decimal? SuperNumber2 { get; set; } = 2;

    public decimal? SuperNumber3 { get; set; } = 3;

    public NestedModel NestedModel1 { get; set; } = new NestedModel();

    public NestedModel NestedModel2 { get; set; } = new NestedModel();

    public IReadOnlyList<NestedModel> ModelCollection { get; set; } = new List<NestedModel>();

    public IReadOnlyList<int> StructCollection { get; set; } = new List<int>();
}

public class NestedModel
{
    public string Text1 { get; set; }

    public string Text2 { get; set; }

    public int Number1 { get; set; }

    public int Number2 { get; set; }

    public decimal? SuperNumber1 { get; set; }

    public decimal? SuperNumber2 { get; set; }
}
