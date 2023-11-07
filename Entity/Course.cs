namespace Entity
{
    public class Cource
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public virtual ICollection<UserCourse> ? UserCourses { get; set; }

       
    }
}
