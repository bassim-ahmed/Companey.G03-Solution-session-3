namespace Company.G03.PL.Services
{
    public class TranslentService:ITranslentService
    {
        public Guid Guid { get; set; }

        public TranslentService()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
