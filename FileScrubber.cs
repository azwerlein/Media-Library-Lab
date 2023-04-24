// Use this to verify that all entries are correct.
public class FileScrubber
{
    public static string ScrubMovies(string readFile)
    {
        try
        {
            string ext = readFile.Split('.').Last();
            string writeFile = readFile.Replace(ext, $"scrubbed.{ext}");

            if (File.Exists(writeFile))
            {
                Program.logger.Info("File already scrubbed");
            }
            else
            {
                Program.logger.Info("File scrub started");
                StreamReader sr = new StreamReader(readFile);
                StreamWriter sw = new StreamWriter(writeFile);
                // Skip the first line
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Movie movie = Movie.Deserialize(line);
                    if (movie == null)
                    {
                        continue;
                    }
                    sw.WriteLine($"{movie.movieId},{movie.title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                }
                sr.Close();
                sw.Close();
                Program.logger.Info("File scrub ended");
            }
            return writeFile;
        }
        catch (Exception e)
        {
            Program.logger.Error(e.Message);
        }
        return "";
    }
}