namespace Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public virtual ICollection<UserCourse> ? UserCourses { get; set; }

       
    }
}
