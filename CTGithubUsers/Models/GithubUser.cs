namespace CTGithubUsers.Models
{
    public class GithubUser
    {
        public string? Name { get; set; } 
        public string? Login { get; set; } 
        public string? Company { get; set; } 
        public int NumberFollowers { get; set; }
        public int NumberRepositories { get; set; }
        public double AverageFollowersPerRepository {
            get {
                // followers/repositories
                // check div 0 
                //
                if (NumberRepositories == 0)
                {
                    return 0;
                } else
                { 
                    return NumberFollowers/NumberRepositories; 
                }
            
            }
                
        }




    }

}
