namespace Odx.Xrm.Core
{
    public class LabeledOptionSetValue
    {
        public LabeledOptionSetValue(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }

        public string Label { get; set; }
    }

    public class LabeledOptionSetValue<T>
    {
        public LabeledOptionSetValue(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public string Label { get; set; }
    }
}