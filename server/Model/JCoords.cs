namespace advisor.Model
{
    public class JCoords {
        public int X { get; set; }
        public int Y { get; set; }
        public int? Z { get; set; }
        public string Label { get; set; }

        public override string ToString() => $"{X},{Y}{(Z.HasValue ? $",{Z}" : "")} {Label}";
    }
}
