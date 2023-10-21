namespace Приемная_комиссия_By_LeXa
{
    public class SpecialComboBoxItem
    {
        public int Id { get; }
        public string Name { get; }

        public SpecialComboBoxItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}