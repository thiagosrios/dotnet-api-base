namespace ApiBase.ViewModels
{
    public class Response
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public bool Result { get; set; }
        public object Data { get; set; }
    }
}
