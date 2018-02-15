using System.Runtime.Serialization;

namespace SBT.Library.Contract
{
    [DataContract]
    public class Members
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public bool Loyal { get; set; }

    }
}
