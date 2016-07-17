namespace AppBeat.CLI.Types
{
    public class PageSpeedTestRes : BaseResponse
    {
        public string TestId
        {
            get; set;
        }

        public TestStatus Status
        {
            get;set;
        }

        public int? LoadTime
        {
            get; set;
        }

        public int? TransferredSize
        {
            get; set;
        }

        public int? DecompressedSize
        {
            get; set;
        }

        public int? Requests
        {
            get; set;
        }

        public Resource[] Resources
        {
            get; set;
        }
    }
}
