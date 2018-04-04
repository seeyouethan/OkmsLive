using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkmsLive.Model
{
    [Serializable]
    public class Course
    {
        public string CourseID { get; set; }
        public string CourseName  { get; set; }
        public string StreamCode { get; set; }
        public List<Chapter> Catalog { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    [Serializable]
    public class Chapter
    {
        public string CataID { get; set; }
        public string CataName { get; set; }
        
        public List<Lecture> LInfo { get; set; }
    }
    [Serializable]
    public class Lecture
    {
        public string LectureID { get; set; }
        public string LectureName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StreamCode { get; set; }

    }
}
