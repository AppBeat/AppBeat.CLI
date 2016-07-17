using System;

namespace AppBeat.CLI.Types
{
    public class Resource
    {
        public string Url
        {
            get; set;
        }

        public int? Status
        {
            get; set;
        }

        public string ContentType
        {
            get; set;
        }

        public DateTime? Start
        {
            get; set;
        }

        public DateTime? End
        {
            get; set;
        }

    }
}
