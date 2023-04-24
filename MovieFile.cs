namespace media
{
    public class MovieFile
    {
        public string Path { get; set; }
        public List<Movie> Movies { get; set; }

        public MovieFile(string filePath)
        {
            Path = filePath;
            Movies = new List<Movie>();
            ReadMovies();
        }

        private void ReadMovies()
        {
            try
            {
                StreamReader sr = new StreamReader(Path);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Movie movie = Movie.Deserialize(line);
                    Movies.Add(movie);
                }
                sr.Close();
                Program.logger.Info("Movies in file {Path}: {Count}", Path, Movies.Count);
            }
            catch (Exception e)
            {
                Program.logger.Error(e.Message);
            }
        }
    }
}