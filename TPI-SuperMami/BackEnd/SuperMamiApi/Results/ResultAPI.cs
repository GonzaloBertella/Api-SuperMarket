namespace SuperMamiApi.Results
{
    public class ResultAPI
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public int ErrorCode { get; set; }
        public string AdditionalInfo { get; set; }
        public object Return { get; set; }
    }
}