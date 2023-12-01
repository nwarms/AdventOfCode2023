namespace AdventOfCode2023
{
    public class FileReader
    {

        public string ReadFile(string filename)
        {
            try
            {
            StreamReader sr = new StreamReader("./Inputs/"+filename);
            return sr.ReadToEnd();

            } catch (FileNotFoundException ex)
            {
                return ex.Message;
            }
            
        }
    }
}
