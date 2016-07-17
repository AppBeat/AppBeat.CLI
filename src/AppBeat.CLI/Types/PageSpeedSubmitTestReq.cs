namespace AppBeat.CLI.Types
{
    public class PageSpeedSubmitTestReq : BaseRequest
    {
        public string Url
        {
            get;set;
        }

        public string Location
        {
            get;set;
        }
    }
}
