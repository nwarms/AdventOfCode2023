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

        public List<string> ReadLines(string filename)
        {
            List<string> lines = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("./Inputs/" + filename);
                string line;
                while ((line = sr.ReadLine()) is not null)
                {
                    lines.Add(line);
                }

            }
            catch (FileNotFoundException ex)
            {
            }
            return lines;
        }
    }
}
