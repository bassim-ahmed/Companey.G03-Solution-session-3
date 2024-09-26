namespace Company.G03.PL.Helper
{
    public class DocumentSettings
    {
        // upload
         public static string Upload(IFormFile file,string folderName)
        {
            //get location of folder

            //string folderpath = $"C:\\Users\\vamprita\\source\\repos\\Companey.G03 Solution\\Companey.G03.PL\\wwwroot\\files\\{foldername}";
            string folderPath=Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}");

            //get file name and make it unique
            string fileName=$"{Guid.NewGuid()}{file.FileName}";

            // get file path : folderpath+filename
            string filePath=Path.Combine(folderPath,fileName);

            // file 
            using var FileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(FileStream);

            return fileName;

        }

        //delete
        public static void Delete( string fileName, string folderName)
        {
            //get location of folder

         
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}",fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
           
        }
    }
}
