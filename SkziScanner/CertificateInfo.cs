namespace SkziScanner
{
    // Класс для хранения данных об одном сертификате СКЗИ
    public class CertificateInfo
    {
        public string SerialNumber { get; set; }
        public string OwnerName { get; set; }
        public string IssuerName { get; set; }
        public string CryptoProVersion { get; set; }
        public bool IsLicenseEmbedded { get; set; }
    }
}
