namespace Glz.GreensJob.Dto
{
    public class CollectObject
    {
        public int id { get; set; }

        public int jobSeekerID { get; set; }

        public int jobID { get; set; }

        public bool status { get; set; }
        public JobObject JobObject { get; set; }
    }
}