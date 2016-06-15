using System.Collections.Generic;

namespace Glz.GreensJob.Dto
{
    public class EnrollObject
    {
        public int id { get; set; }

        public int jobSeekerID { get; set; }

        public JobSeekerObject JobSeeker { get; set; }

        public IList<EnrollDetailObject> EnrollDetails { get; set; }

        public int jobID { get; set; }

        public int introducer { get; set; }

        public bool experienced { get; set; }

        public string name { get; set; }

        public string mobile { get; set; }

        public int state { get; set; }

        public int method { get; set; }
        public JobObject Job { get; set; }

    }
}